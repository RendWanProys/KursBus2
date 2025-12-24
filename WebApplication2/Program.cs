using KursBus2.Models;
using KursBus2.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Simple Test API", Version = "v1" });
    options.AddSecurityDefinition("JWT_OR_COOKIE", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id=JwtBearerDefaults.AuthenticationScheme
                    }
                },
                new string[]{}
            }
        });
});

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<KursProjectContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<RaceService, RaceService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSimmetricSecurutyKey(),
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapPost("/login", async (UserData user, KursProjectContext db) =>
{
    UserData? userdata = await db.UserDates!.FirstOrDefaultAsync(p => p.Email == user.Email);
    string Password = AuthOptions.GetHash(user.PassWord);
    if (userdata is null) return Results.Unauthorized();
    if (userdata.PassWord != Password) return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };
    var jwt = new JwtSecurityToken
    (
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSimmetricSecurutyKey(), SecurityAlgorithms.HmacSha256));
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = userdata.Email
    };
    return Results.Json(response);
}
);
app.MapPost("/register", async (UserData user, KursProjectContext db) =>
{
    user.PassWord = AuthOptions.GetHash(user.PassWord);
    db.UserDates.Add(user);
    await db.SaveChangesAsync();
    UserData createdUser = db.UserDates.FirstOrDefault(p => p.Email == user.Email)!;
    return Results.Ok(createdUser);
});
var context = app.Services.CreateScope().ServiceProvider.
    GetRequiredService<KursProjectContext>();
SeedData.SeedDatabase(context);
app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    const string KEY = "mysupersecret_secretsecretkey!123";
    public static SymmetricSecurityKey GetSimmetricSecurutyKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    public static string GetHash(string plaintext)
    {
        var sha = new SHA1Managed();
        byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(plaintext));
        return Convert.ToBase64String(hash);
    }
}


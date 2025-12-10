using KursBus2.Models;
using KursBus2.Services;
using KursBus2.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
    UserData? person = await db.UserData!.FirstOrDefaultAsync(p => p.Email == user.Email);
    string Password = AuthOptions.GetHash(user.PassWord);
    if (person is null) return Results.Unauthorized();
    if (person.PassWord != Password) return Results.Unauthorized();
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
        username = person.Email
    };
    return Results.Json(response);
}
);
app.MapPost("/register", async (UserData user, KursProjectContext db) =>
{
    user.PassWord = AuthOptions.GetHash(user.PassWord);
    db.UserData.Add(user);
    await db.SaveChangesAsync();
    UserData createdUser = db.UserData.FirstOrDefault(p => p.Email == user.Email)!;
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


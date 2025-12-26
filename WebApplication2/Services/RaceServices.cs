using KursBus2.Models;
using Microsoft.EntityFrameworkCore;

namespace KursBus2.Services
{
    public class RaceService : IService<Race>
    {
        private readonly KursProjectContext db;
        public RaceService(KursProjectContext _db) => this.db = _db;
        public async Task<IEnumerable<Race>> GetAll()
        {
            return await db.Races
            //.Include(r => r.Schedule)
            .ToListAsync(); 
        }
        public async Task<Race> GetById(int id)
        {
            return await db.Races
                .Include(r => r.Schedule)
                .FirstOrDefaultAsync(r => r.RaceId == id);
        }
        public async Task Create(Race entity)
        {
            if (entity.TripId.HasValue && entity.TripId > 0)
            {
                var scheduleExists = await db.Schedules
                    .AnyAsync(s => s.TripId == entity.TripId);

                if (!scheduleExists)
                    throw new ArgumentException($"Маршрут с ID {entity.TripId} не найден");
            }
            db.Races.Add(entity);
            await db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var chit = await db.Races.FindAsync(id);
            if (chit != null)
            {
                db.Races.Remove(chit);
                await db.SaveChangesAsync();
            }
        }
        public async Task Update(Race entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.Races.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}

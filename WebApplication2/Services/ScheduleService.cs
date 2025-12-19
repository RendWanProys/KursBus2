using KursBus2.Models;
using Microsoft.EntityFrameworkCore;

namespace KursBus2.Services
{
    public class ScheduleService : IService<Schedule>
    {
        private readonly KursProjectContext db;
        public ScheduleService(KursProjectContext _db) => this.db = _db;
        public async Task<IEnumerable<Schedule>> GetAll()
        {
            return await db.Schedules.ToListAsync();
        }
        public async Task<Schedule> GetById(int id)
        {
            return await db.Schedules.FindAsync(id);
        }
        public async Task Create(Schedule entity)
        {
            db.Schedules.Add(entity);
            await db.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var chit = await db.Schedules.FindAsync(id);
            if (chit != null)
            {
                db.Schedules.Remove(chit);
                await db.SaveChangesAsync();
            }
        }
        public async Task Update(Schedule entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.Schedules.Update(entity);
            await db.SaveChangesAsync();
        }
    }
}

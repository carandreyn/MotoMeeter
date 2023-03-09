using Microsoft.EntityFrameworkCore;
using MotoMeeter.Data;
using MotoMeeter.Interfaces;
using MotoMeeter.Models;

namespace MotoMeeter.Repository
{
    public class MeetupRepository : IMeetupRepository
    {
        private readonly ApplicationDbContext _context;

        public MeetupRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Meetup meetup)
        {
            _context.Add(meetup);
            return Save();
        }

        public bool Delete(Meetup meetup)
        {
            _context.Remove(meetup);
            return Save();
        }

        public async Task<IEnumerable<Meetup>> GetAll()
        {
            return await _context.Meetups.ToListAsync();
        }

        public async Task<IEnumerable<Meetup>> GetAllMeetupsByCity(string city)
        {
            return await _context.Meetups.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public async Task<Meetup> GetByIdAsync(int id)
        {
            return await _context.Meetups.Include(i => i.Address).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Meetup> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Meetups.Include(i => i.Address).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Meetup meetup)
        {
            _context.Update(meetup);
            return Save();
        }
    }
}

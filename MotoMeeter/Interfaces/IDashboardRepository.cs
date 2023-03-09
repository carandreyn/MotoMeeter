using MotoMeeter.Models;
using System.Diagnostics;

namespace MotoMeeter.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Meetup>> GetAllUserMeetups();
        Task<List<Club>> GetAllUserClubs();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}

using MotoMeeter.Data.Enum;
using MotoMeeter.Models;

namespace MotoMeeter.Interfaces
{
    public interface IMeetupRepository
    {
        Task<int> GetCountAsync();

        Task<int> GetCountByCategoryAsync(MeetupCategory category);

        Task<Meetup?> GetByIdAsync(int id);

        Task<Meetup?> GetByIdAsyncNoTracking(int id);

        Task<IEnumerable<Meetup>> GetAll();

        Task<IEnumerable<Meetup>> GetAllMeetupsByCity(string city);

        Task<IEnumerable<Meetup>> GetSliceAsync(int offset, int size);

        Task<IEnumerable<Meetup>> GetMeetupsByCategoryAndSliceAsync(MeetupCategory category, int offset, int size);

        bool Add(Meetup meetup);

        bool Update(Meetup meetup);

        bool Delete(Meetup meetup);

        bool Save();
    }
}

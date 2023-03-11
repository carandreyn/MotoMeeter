using MotoMeeter.Models;

namespace MotoMeeter.ViewModels
{
    public class IndexMeetupViewModel
    {
        public IEnumerable<Meetup> Meetups { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public int TotalMeetups { get; set; }
        public int Category { get; set; }
        public bool HasPreviousPage => Page > 1;
        public bool HasNextPage => Page < TotalPages;
    }
}
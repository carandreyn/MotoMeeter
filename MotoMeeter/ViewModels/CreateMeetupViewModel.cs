using MotoMeeter.Data.Enum;
using MotoMeeter.Models;

namespace MotoMeeter.ViewModels
{
    public class CreateMeetupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public MeetupCategory MeetupCategory { get; set; }
        public string AppUserId { get; set; }
    }
}

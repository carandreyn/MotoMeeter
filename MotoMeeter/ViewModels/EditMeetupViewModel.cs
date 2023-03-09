using MotoMeeter.Data.Enum;
using MotoMeeter.Models;

namespace MotoMeeter.ViewModels
{
    public class EditMeetupViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public MeetupCategory MeetupCategory { get; set; }
    }
}

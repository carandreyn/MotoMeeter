using MotoMeeter.Models;

namespace MotoMeeter.ViewModels
{
    public class EditProfileViewModel
    {
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public IFormFile? Image { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace MotoMeeter.ViewModels
{
    public class HomeUserCreateViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

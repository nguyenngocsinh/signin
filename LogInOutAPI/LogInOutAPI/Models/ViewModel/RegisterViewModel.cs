using System.ComponentModel.DataAnnotations;

namespace LogInOutAPI.Models.ViewModel
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string confirmPassword { get; set; }
        public int IsActive { get; set; }
    }
}

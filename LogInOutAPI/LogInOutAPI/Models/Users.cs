using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogInOutAPI.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(250)]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public int IsActive { get; set; }

       
        public ICollection<UserCompany> UserCompanies { get; set; }
    }
}

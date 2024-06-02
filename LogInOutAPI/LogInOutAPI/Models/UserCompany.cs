using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogInOutAPI.Models
{
    [Table("UserCompany")]
    public class UserCompany
    {
        [Key]
        public int UserCompanyId { get; set; }
        public string Company { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}

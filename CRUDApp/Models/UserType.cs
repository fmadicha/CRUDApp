using System.ComponentModel.DataAnnotations;

namespace CRUDApp.Models
{
    public class UserType
    {
        public int Id { get; set; }

        [Display(Name="User Type")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}

using CRUDApp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter your First Name")]
        [Display(Name = "First Name")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Please enter your Last Name")]
        [StringLength(100)]
        [Display(Name ="Last Name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please enter age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please choose gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please choose profile image")]
        public string ProfilePicture { get; set; }

        [Required]
        [Display(Name ="User Type")]
        public int UserTypeId { get; set; }

        [ValidateNever]
        [ForeignKey("UserTypeId")]
        public UserType UserType { get; set; }

    }

}

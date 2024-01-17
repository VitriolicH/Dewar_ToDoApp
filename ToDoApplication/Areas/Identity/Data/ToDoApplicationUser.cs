using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToDoApplication.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ToDoApplicationUser class
public class ToDoApplicationUser : IdentityUser
{
    [Required(ErrorMessage = "Please Enter Your Full Name.")]
    [DataType(DataType.Text)]
    [Display(Name = "Name")]
    public string FullName { get; set; } = string.Empty;
}


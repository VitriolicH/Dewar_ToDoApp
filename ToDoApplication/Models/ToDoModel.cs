using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApplication.Areas.Identity.Data;

namespace ToDoApplication.Models;

public class ToDoModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Please Enter The Task Name.")]
    public string TaskName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please Select a Due Date ")]
    public DateTime DueDate { get; set; }
    public string IsCompleted { get; set; } = "Pending";
    public string UserId { get; set; } = string.Empty;

    [ValidateNever]
    public ToDoApplicationUser User { get; set; } = null!;

    public bool TimeOver => IsCompleted == "Pending" && DueDate < DateTime.Today;
}

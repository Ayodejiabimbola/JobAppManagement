using JobAppManagement.Data.Enum;
using JobAppManagement.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JobAppManagement.Models.Applicant;

public class ApplicantViewModel
{
    [Display(Name = "First Name")]
    [Required]
    public string FirstName { get; set; } = default!;

    [Display(Name = "Last Name")]
    [Required]
    public string LastName { get; set; } = default!;

    [Display(Name = "Gender")]
    [Required(ErrorMessage ="Please select a gender")]
    public Gender Gender { get; set; } = default!;

    [Display(Name = "Email")]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = default!;

    [Display(Name = "Phone Number")]
    [Required]
    public string PhoneNumber { get; set; } = default!;

    [Display(Name = "States")]
    [Required(ErrorMessage ="Please select a state")]
    public int StateId { get; set; }
    public List<SelectListItem> States { get; set; } = default!;
}
using JobAppManagement.Data;
using JobAppManagement.Data.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JobAppManagement.Models.Applicant;

public class ApplicantDetailViewModel
{
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = default!;

    [Display(Name = "Last Name")]
    public string LastName { get; set; } = default!;

    [Display(Name = "Email")]
    public string Email { get; set; } = default!;

    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; } = default!;

    [Display(Name = "Gender")]
    public Gender Gender { get; set; } = default!;
}

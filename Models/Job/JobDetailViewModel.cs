using JobAppManagement.Data;
using JobAppManagement.Data.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace JobAppManagement.Models.Job;

public class JobDetailViewModel
{
    [Display(Name = "Job Name")]
    public string Name { get; set; } = default!;

    [Display(Name = "Job Description")]
    public string? Description { get; set; }
}

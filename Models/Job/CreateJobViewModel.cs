using System.ComponentModel.DataAnnotations;

namespace JobAppManagement.Models.Job;

public class CreateJobViewModel
{
    [Display(Name = "Job Name")]
    [Required]
    public string Name { get; set; } = default!;

    [Display(Name = "Job Description")]
    [Required]
    public string? Description { get; set; } = default!;
}
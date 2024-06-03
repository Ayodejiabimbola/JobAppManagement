
using JobAppManagement.Data.Enum;

namespace JobAppManagement.Data;
public class Applicant : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public DateTime DOB { get; set; }
    public Gender Gender { get; set; }
    public string UserId { get; internal set; } = default!;
    public int StateId { get; internal set; }
    public int JobId { get; internal set; } 
}
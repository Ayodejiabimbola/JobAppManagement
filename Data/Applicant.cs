using JobAppManagement.Data;

public class TeamMember : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public DateTime DOB { get; set; }
    public Gender Gender { get; set; }
}
using JobAppManagement.Data;

public class ApplicantJob : IAuditBase
{
    public int TeamMemberId { get; set; }
    public int EventId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
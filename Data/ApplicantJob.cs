namespace JobAppManagement.Data;
public class ApplicantJob : IAuditBase
{
    public int ApplicantId { get; set; }
    public int JobId { get; set; }
    public DateTime AppliedOn { get; set; }
    public DateTime? LastUpdated { get; set; }
}
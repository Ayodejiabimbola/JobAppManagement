
public abstract class BaseEntity : IAuditBase
{
    public int Id { get; set; }
    public DateTime AppliedOn { get; set; }
    public DateTime? LastUpdated { get; set; }
}
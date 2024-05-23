public interface IAuditBase
{
    public DateTime AppliedOn { get; set; }
    public DateTime? LastUpdated { get; set; }
}
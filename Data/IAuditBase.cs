public interface IAuditBase
{
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
namespace JobAppManagement.Data;

public abstract class BaseEntity : IAuditBase
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
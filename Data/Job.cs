// #pragma warning disable CA1050 // Declare types in namespaces
public class Job : BaseEntity
// #pragma warning restore CA1050 // Declare types in namespaces
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
using JobAppManagement.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;


namespace JobAppManagement.Context;

public class JobAppManagementContext(DbContextOptions<JobAppManagementContext> options) : IdentityDbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.Entity<ApplicantJob>()
        .HasNoKey();
    }

    public DbSet<Applicant> Applicant { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<ApplicantJob> ApplicantJob { get; set; }
    public DbSet<Job> Job { get; set; }
}


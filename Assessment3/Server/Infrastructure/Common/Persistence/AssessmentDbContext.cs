using System.Reflection;
using Assessment3.Server.Domain.Common;
using Assessment3.Server.Domain.Events;
using Assessment3.Server.Domain.UserEvents;
using Assessment3.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Assessment3.Server.Infrastructure.Common.Persistence;

public class AssessmentDbContext : DbContext
{
    public AssessmentDbContext(DbContextOptions<AssessmentDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserEvent> UserEvents { get; set; }
    public DbSet<Event>  Events{ get; set; }
}
using Assessment3.Server.Domain;
using Assessment3.Server.Domain.Events;
using Assessment3.Server.Domain.UserEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Assessment3.Server.Infrastructure.Common.Persistence.Configurations;

public class UserEventConfiguration : IEntityTypeConfiguration<UserEvent>
{
    public void Configure(EntityTypeBuilder<UserEvent> builder)
    {
        ConfigureEventUsersTable(builder);
    }
    
    private void ConfigureEventUsersTable(EntityTypeBuilder<UserEvent> builder)
    {
        builder.ToTable("UserEvents");
        builder.HasKey(ue => new { ue.UserId, ue.EventId });

        builder.HasOne(ue => ue.User)
            .WithMany(u => u.UserEvents)
            .HasForeignKey(ue => ue.UserId);

        builder.HasOne(ue => ue.Event)
            .WithMany(e => e.UserEvents)
            .HasForeignKey(ue => ue.EventId);
        
        // Add a unique constraint for UserId and EventId
        builder
            .HasIndex(ue => new { ue.UserId, ue.EventId })
            .IsUnique();
    }
}
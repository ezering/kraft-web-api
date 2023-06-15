using Domain.Entities;
using Domain.ValueObjects.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id)
            .HasConversion(x => x.Value, 
                x => UserId.Create(x).Value);
        
        builder.HasIndex(user => user.Username).IsUnique();
        builder.Property(user => user.Username)
            .HasConversion(x => x.Value, 
                x => Username.Create(x).Value)
            .IsRequired();
        
        builder.Property(user => user.Firstname)
            .IsRequired()
            .HasConversion(x => x.Value, 
                x => Firstname.Create(x).Value);
        
        builder.Property(user => user.Lastname)
            .HasConversion(x => x.Value, 
                x => Lastname.Create(x).Value)
            .IsRequired();
        
        builder.HasIndex(user => user.Email).IsUnique();
        builder.Property(user => user.Email)
            .HasConversion(x => x.Value, 
                x => Email.Create(x).Value)
            .IsRequired();
        
        builder.Property(user => user.Password)
            .HasConversion(x => x.Value, 
                x => Password.Create(x).Value)
            .IsRequired();
        
        builder.Property(user => user.ProfilePicture)
            .HasConversion(x 
                => x.Value, x => ProfilePicture.Create(x).Value)
            .IsRequired();

        builder.Property(user => user.Roles)
            .HasPostgresArrayConversion(x => x.Value, 
                x => Role.Create(x).Value)
            .IsRequired();

        builder.Property(user => user.DateCreated)
            .IsRequired();

        builder.Property(user => user.DateUpdated);
        
        builder.Property(user => user.DateDeleted);
        

    }
}
namespace StudentFiles.Infrastructure.Common.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StudentFiles.Infrastructure.Data.Models;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(name: "User");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Uid).HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnType("BIT").HasDefaultValue(false);
            builder.Property(x => x.Username).HasColumnType("NVARCHAR").HasMaxLength(77).IsRequired();
            builder.Property(x => x.HashedPassword).HasColumnType("NVARCHAR").HasMaxLength(60).IsRequired();
            builder.Property(x => x.Role).HasColumnType("NVARCHAR").HasMaxLength(9).IsRequired();
        }
    }
}

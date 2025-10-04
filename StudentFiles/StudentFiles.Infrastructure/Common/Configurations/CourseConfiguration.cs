namespace StudentFiles.Infrastructure.Common.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StudentFiles.Infrastructure.Data.Models;

    public  class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.ToTable(name: "Course");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Uid).HasColumnName("Uid").HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").HasColumnType("BIT").HasDefaultValue(false);
            builder.Property(x => x.CourseId).HasColumnName("CourseId").HasColumnType("NVARCHAR").HasMaxLength(7).IsRequired();
            builder.Property(x => x.CourseName).HasColumnName("CourseName").HasColumnType("NVARCHAR").HasMaxLength(30).IsRequired();

            builder.Property(x => x.ProfessorFk).HasColumnName("ProfessorFk").HasColumnType("INT").IsRequired();

            builder.HasOne(x => x.Professor).WithMany().HasForeignKey(x => x.ProfessorFk).OnDelete(DeleteBehavior.Restrict);
        }
    }
}

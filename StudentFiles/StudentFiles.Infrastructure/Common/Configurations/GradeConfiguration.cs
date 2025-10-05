namespace StudentFiles.Infrastructure.Common.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using StudentFiles.Infrastructure.Data.Models;

    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.ToTable(name: "Grade");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Uid).HasColumnName("Uid").HasColumnType("UNIQUEIDENTIFIER").IsRequired();
            builder.Property(x => x.IsDeleted).HasColumnName("IsDeleted").HasColumnType("BIT").HasDefaultValue(false);
            builder.Property(x => x.Value).HasColumnName("Value").HasColumnType("INT").IsRequired();
            builder.Property(x => x.DateAssigned).HasColumnName("DateAssigned").HasColumnType("DATE").IsRequired();

            builder.Property(x => x.StudentFk).HasColumnName("StudentFk").HasColumnType("INT").IsRequired();
            builder.Property(x => x.CourseFk).HasColumnName("CourseFk").HasColumnType("INT").IsRequired();
            builder.Property(x => x.ProfessorFk).HasColumnName("ProfessorFk").HasColumnType("INT").IsRequired();

            builder.HasOne(x => x.Student).WithMany().HasForeignKey(x => x.StudentFk);
            builder.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseFk);
            builder.HasOne(x => x.Professor).WithMany().HasForeignKey(x => x.ProfessorFk);
        }
    }
}

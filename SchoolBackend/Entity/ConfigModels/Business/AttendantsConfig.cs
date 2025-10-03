using Entity.ConfigModels.global;
using Entity.Model.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entity.ConfigModels.Business
{
    public class AttendantsConfig : IEntityTypeConfiguration<Attendants>
    {
        public void Configure(EntityTypeBuilder<Attendants> builder)
        {
            // Tabla y esquema
            builder.ToTable("attendants", schema: "business");

            // PK (de ABaseEntity)
            builder.HasKey(a => a.Id);

            // Columnas
            builder.Property(a => a.Id)
                   .HasColumnName("id")
                   .IsRequired();

            builder.Property(a => a.PersonId)
                   .HasColumnName("person_id")
                   .IsRequired();

            // Auditoría / estado comunes
            builder.MapBaseModel();

            builder.HasOne(a => a.Person)
                   .WithMany(p => p.Attendants)         // agrega ICollection<Attendants> en Person si la quieres
                   .HasForeignKey(a => a.PersonId)
                   .HasConstraintName("fk_attendants_person")
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

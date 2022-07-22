using GeradorTestes.Dominio.ModuloMateria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorTestes.Infra.Orm.ModuloMateria
{
    public class MapeadorMateriaOrm : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> builder)
        {
            builder.ToTable("TBMateria");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).IsRequired().HasColumnType("varchar(100)");
            builder.Property(x => x.Serie).HasConversion<int>();
            builder.HasOne(x => x.Disciplina);
        }
    }
}

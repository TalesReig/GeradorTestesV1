using GeradorTestes.Dominio.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorTestes.Infra.Orm.ModuloDisciplina
{
    public class MapeadorDisciplinaOrm : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> builder)
        {
            builder.ToTable("TBDisciplina");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();
        }
    }
}

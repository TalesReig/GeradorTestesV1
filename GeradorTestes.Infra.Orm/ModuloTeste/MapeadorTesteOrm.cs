using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorTestes.Infra.Orm.ModuloTeste
{
    internal class MapeadorTesteOrm : IEntityTypeConfiguration<Teste>
    {
        public void Configure(EntityTypeBuilder<Teste> entidade)
        {
            entidade.ToTable("TBTeste");
            entidade.Property(x => x.Id).ValueGeneratedNever();
            entidade.Property(x => x.Titulo).IsRequired().HasColumnType("varchar(250)");
            entidade.Property(x => x.QuantidadeQuestoes).IsRequired();
            entidade.Property(x => x.DataGeracao).IsRequired();
            entidade.Property(x => x.Provao).IsRequired();
            entidade.HasMany(x => x.Questoes);

            entidade.HasOne(x => x.Disciplina)
                .WithMany().OnDelete(DeleteBehavior.NoAction);

            entidade.HasOne(x => x.Materia)
                .WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}

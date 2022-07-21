using GeradorTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorTestes.Infra.Orm.ModuloQuestao
{
    internal class MapeadorAlternativaOrm : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> entidade)
        {
            entidade.ToTable("TBAlternativa");
            entidade.Property(x => x.Id).ValueGeneratedNever();
            entidade.Property(x => x.Resposta).IsRequired().HasColumnType("varchar(500)");
            entidade.Property(x => x.Letra).IsRequired().HasColumnType("char(1)");
            entidade.Property(x => x.Correta).IsRequired();

            entidade.HasOne(x => x.Questao)
                .WithMany(x => x.Alternativas)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TBAlternativa_TBQuestao");
        }
    }
}

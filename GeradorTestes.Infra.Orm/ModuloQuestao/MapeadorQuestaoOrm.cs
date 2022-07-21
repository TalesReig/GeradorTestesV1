using GeradorTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorTestes.Infra.Orm.ModuloQuestao
{
    internal class MapeadorQuestaoOrm : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> entidade)
        {
            entidade.ToTable("TBQuestao");
            entidade.Property(x => x.Id).ValueGeneratedNever();
            entidade.Property(x => x.Enunciado).IsRequired().HasColumnType("varchar(500)");
            entidade.HasOne(x => x.Materia);
            entidade.HasMany(x => x.Testes);
        }
    }
}

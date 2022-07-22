using GeradorTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorTestes.Infra.Orm.ModuloQuestao
{
    internal class MapeadorQuestaoOrm : IEntityTypeConfiguration<Questao>
    {
        public void Configure(EntityTypeBuilder<Questao> builder)
        {
            builder.ToTable("TBQuestao");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Enunciado).IsRequired().HasColumnType("varchar(500)");
            builder.HasOne(x => x.Materia);
            builder.HasMany(x => x.Testes);
        }
    }
}

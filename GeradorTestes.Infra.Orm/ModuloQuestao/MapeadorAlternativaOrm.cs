using GeradorTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModuloQuestao
{
    internal class MapeadorAlternativaOrm : IEntityTypeConfiguration<Alternativa>
    {
        public void Configure(EntityTypeBuilder<Alternativa> builder)
        {
            builder.ToTable("TBAlternativa");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Resposta).IsRequired().HasColumnType("varchar(500)");
            builder.Property(x => x.Letra).IsRequired().HasColumnType("char(1)");
            builder.Property(x => x.Correta).IsRequired();

            builder.HasOne(x => x.Questao)
                .WithMany(x => x.Alternativas)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TBAlternativa_TBQuestao");
        }
    }
}

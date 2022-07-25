using GeradorTestes.Dominio.ModuloTeste;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModutoTeste
{
    public class MapeadorTesteOrm : IEntityTypeConfiguration<Teste>
    {
        public void Configure(EntityTypeBuilder<Teste> builder)
        {
            builder.ToTable("TBTeste");
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Titulo).IsRequired().HasColumnType("varchar(250)");
            builder.Property(x => x.QuantidadeQuestoes).IsRequired();
            builder.Property(x => x.DataGeracao).IsRequired();
            builder.Property(x => x.Provao).IsRequired();
            builder.HasMany(x => x.Questoes);

            builder.HasOne(x => x.Disciplina)
                .WithMany().OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Materia)
                .WithMany()
                .IsRequired(false) 
                .HasForeignKey(x => x.MateriaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

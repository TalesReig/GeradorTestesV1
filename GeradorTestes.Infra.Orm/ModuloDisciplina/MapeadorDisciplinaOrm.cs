using GeradorTestes.Dominio.ModuloDisciplina;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModuloDisciplina
{
    internal class MapeadorDisciplinaOrm : IEntityTypeConfiguration<Disciplina>
    {
        public void Configure(EntityTypeBuilder<Disciplina> entidade)
        {
            entidade.ToTable("TBDisciplina");
            entidade.Property(x => x.Id).ValueGeneratedNever();
            entidade.Property(x => x.Nome).HasColumnType("varchar(100)").IsRequired();
        }
    }
}

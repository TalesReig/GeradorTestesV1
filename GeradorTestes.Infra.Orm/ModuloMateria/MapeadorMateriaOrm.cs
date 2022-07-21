using GeradorTestes.Dominio.ModuloMateria;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorTestes.Infra.Orm.ModuloMateria
{
    internal class MapeadorMateriaOrm : IEntityTypeConfiguration<Materia>
    {
        public void Configure(EntityTypeBuilder<Materia> entidade)
        {
            entidade.ToTable("TBMateria");
            entidade.Property(x => x.Id).ValueGeneratedNever();
            entidade.Property(x => x.Nome).IsRequired().HasColumnType("varchar(100)");
            entidade.Property(x => x.Serie).HasConversion<int>();
            entidade.HasOne(x => x.Disciplina);
        }
    }
}

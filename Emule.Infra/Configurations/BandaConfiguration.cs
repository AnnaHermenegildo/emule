using Emule.Domain.Model;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Infra.Configurations
{
    public class BandaConfiguration : IEntityTypeConfiguration<Banda>
    {
        public void Configure(EntityTypeBuilder<Banda> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Nome)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(b => b.Genero)
                   .HasMaxLength(100);

            builder.Property(b => b.DataCriada)
                   .IsRequired();
        }
    }
}

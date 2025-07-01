using Emule.Domain.Model.Repository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emule.Domain.Model;

namespace Emule.Infra.Configurations
{
    public class MusicaConfiguration : IEntityTypeConfiguration<Musica>
    {
        public void Configure(EntityTypeBuilder<Musica> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Titulo)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(m => m.Duracao)
                   .IsRequired();

            builder.HasOne(m => m.Banda)
                   .WithMany(b => b.Musicas)
                   .HasForeignKey(m => m.BandaId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

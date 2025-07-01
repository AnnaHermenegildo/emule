using Emule.Domain;
using Emule.Domain.Model;
using Emule.Domain.Model.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Emule.Infra
{
    public class EmuleDbContext : DbContext
    {
        public EmuleDbContext(DbContextOptions<EmuleDbContext> options)
            : base(options)
        {
        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Assinatura> Assinaturas { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<Musica> Musicas { get; set; }
        public DbSet<Banda> Bandas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmuleDbContext).Assembly);
            // Assinatura -> Plano
            modelBuilder.Entity<Assinatura>()
                .HasOne(a => a.Plano)
                .WithMany()
                .HasForeignKey(a => a.IdPlano);


            // Usuario -> Playlists
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Playlists)
                .WithOne(p => p.Usuario)
                .HasForeignKey(p => p.UsuarioId);

            // Usuario -> Músicas favoritas (M:N)
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Favoritos)
                .WithMany();

            // Usuario -> Bandas favoritas (M:N)
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.BandasFavoritas)
                .WithMany();

            // Playlist -> Músicas (M:N)
            modelBuilder.Entity<Playlist>()
                .HasMany(p => p.Musicas)
                .WithMany();

            // Musica -> Banda (1:N)
            modelBuilder.Entity<Musica>()
                .HasOne(m => m.Banda)
                .WithMany(b => b.Musicas)
                .HasForeignKey(m => m.BandaId);


            modelBuilder.Entity<Plano>().Property(b => b.Valor)
                   .HasPrecision(18, 2);
        }
    }
}

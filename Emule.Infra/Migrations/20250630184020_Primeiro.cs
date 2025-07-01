using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emule.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Primeiro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bandas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataCriada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bandas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    TemAnuncios = table.Column<bool>(type: "bit", nullable: false, defaultValue: false) // Coluna nova
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.Id);
                });

            // Seed dos 3 planos com TemAnuncios correto
            migrationBuilder.InsertData(
                table: "Planos",
                columns: new[] { "Id", "Nome", "Descricao", "Valor", "Duration", "TemAnuncios" },
                values: new object[,]
                {
                    { Guid.NewGuid(), "Free", "Plano gratuito com anúncios", 0.00m, TimeSpan.Parse("30.00:00:00"), true },
                    { Guid.NewGuid(), "Básico", "Plano básico sem anúncios", 35.00m, TimeSpan.Parse("30.00:00:00"), false },
                    { Guid.NewGuid(), "Premium", "Plano premium sem anúncios", 60.00m, TimeSpan.Parse("60.00:00:00"), false }
                });

            migrationBuilder.CreateTable(
                name: "Musicas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false),
                    BandaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musicas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musicas_Bandas_BandaId",
                        column: x => x.BandaId,
                        principalTable: "Bandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assinaturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdPlano = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Inicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assinaturas_Planos_IdPlano",
                        column: x => x.IdPlano,
                        principalTable: "Planos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssinaturaAtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Assinaturas_AssinaturaAtualId",
                        column: x => x.AssinaturaAtualId,
                        principalTable: "Assinaturas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BandaUsuario",
                columns: table => new
                {
                    BandasFavoritasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BandaUsuario", x => new { x.BandasFavoritasId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_BandaUsuario_Bandas_BandasFavoritasId",
                        column: x => x.BandasFavoritasId,
                        principalTable: "Bandas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BandaUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicaUsuario",
                columns: table => new
                {
                    FavoritosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaUsuario", x => new { x.FavoritosId, x.UsuarioId });
                    table.ForeignKey(
                        name: "FK_MusicaUsuario_Musicas_FavoritosId",
                        column: x => x.FavoritosId,
                        principalTable: "Musicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicaUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Playlists_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicaPlaylist",
                columns: table => new
                {
                    MusicasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlaylistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicaPlaylist", x => new { x.MusicasId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_MusicaPlaylist_Musicas_MusicasId",
                        column: x => x.MusicasId,
                        principalTable: "Musicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicaPlaylist_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_IdPlano",
                table: "Assinaturas",
                column: "IdPlano");

            migrationBuilder.CreateIndex(
                name: "IX_Assinaturas_UsuarioId",
                table: "Assinaturas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_BandaUsuario_UsuarioId",
                table: "BandaUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaPlaylist_PlaylistId",
                table: "MusicaPlaylist",
                column: "PlaylistId");

            migrationBuilder.CreateIndex(
                name: "IX_Musicas_BandaId",
                table: "Musicas",
                column: "BandaId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicaUsuario_UsuarioId",
                table: "MusicaUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UsuarioId",
                table: "Playlists",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_AssinaturaAtualId",
                table: "Usuarios",
                column: "AssinaturaAtualId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assinaturas_Usuarios_UsuarioId",
                table: "Assinaturas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assinaturas_Planos_IdPlano",
                table: "Assinaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Assinaturas_Usuarios_UsuarioId",
                table: "Assinaturas");

            migrationBuilder.DropTable(
                name: "BandaUsuario");

            migrationBuilder.DropTable(
                name: "MusicaPlaylist");

            migrationBuilder.DropTable(
                name: "MusicaUsuario");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Musicas");

            migrationBuilder.DropTable(
                name: "Bandas");

            migrationBuilder.DropTable(
                name: "Planos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Assinaturas");
        }

        public partial class AlterarPlanoParaDuracaoEmDias : Migration
        {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                // Remove a coluna antiga
                migrationBuilder.DropColumn(
                    name: "Duration",
                    table: "Planos");

                // Adiciona a nova coluna
                migrationBuilder.AddColumn<int>(
                    name: "DuracaoDias",
                    table: "Planos",
                    type: "int",
                    nullable: false,
                    defaultValue: 30); // valor default genérico

                // Atualiza valores conforme necessário
                migrationBuilder.Sql(@"
                UPDATE Planos SET DuracaoDias = 30 WHERE Nome = 'Free' OR Nome = 'Básico';
                UPDATE Planos SET DuracaoDias = 60 WHERE Nome = 'Premium';
            ");
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                // Remove a nova coluna
                migrationBuilder.DropColumn(
                    name: "DuracaoDias",
                    table: "Planos");

                // Recria a coluna antiga
                migrationBuilder.AddColumn<TimeSpan>(
                    name: "Duration",
                    table: "Planos",
                    type: "time",
                    nullable: false,
                    defaultValue: new TimeSpan(1, 0, 0)); // fallback

                // OBS: dados antigos não serão restaurados automaticamente.
            }
        }
    }
}

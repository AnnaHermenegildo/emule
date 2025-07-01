using Emule.ApplicationLayer.DTOs;
using Emule.Domain.Model;
using Emule.Domain.Model.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Emule.ApplicationLayer.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMusicaRepository _musicaRepo;
        private readonly IBandaRepository _bandaRepo;
        private readonly IPlanoRepository _planoRepo;
        private readonly IAssinaturaRepository _assinaturaRepo;

        public UsuarioService(
            IUsuarioRepository usuarioRepo,
            IMusicaRepository musicaRepo,
            IBandaRepository bandaRepo,
            IPlanoRepository planoRepo,
            IAssinaturaRepository assinaturaRepo)
        {
            _usuarioRepo = usuarioRepo;
            _musicaRepo = musicaRepo;
            _bandaRepo = bandaRepo;
            _planoRepo = planoRepo;
            _assinaturaRepo = assinaturaRepo;
        }

        public async Task<UsuarioDto?> LoginAsync(string username, string senha)
        {
            var usuario = await _usuarioRepo.ObterPorUsernameAsync(username);

            if (usuario == null || usuario.Senha != senha)
                return null;

            var usuarioDto = new UsuarioDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
            };

            return usuarioDto;
        }

        public async Task RegistrarAsync(NovoUsuarioDto novoUsuario)
        {
            try
            {
                // Verifica se o usuário já existe
                var existente = await _usuarioRepo.ObterPorUsernameAsync(novoUsuario.Username);
                if (existente is not null)
                    throw new Exception("Usuário já existe.");

                // Busca o plano escolhido
                var plano = await _planoRepo.ObterPorIdAsync(novoUsuario.PlanoId);
                if (plano is null)
                    throw new Exception("Plano inválido.");

                // Cria o novo usuário SEM AssinaturaAtualId e sem assinaturas adicionadas ainda
                var novo = new Usuario(novoUsuario.Username, novoUsuario.Senha);

                // Salva o usuário primeiro (sem assinaturas)
                await _usuarioRepo.AdicionarAsync(novo);

                // Cria a assinatura vinculada ao usuário recém criado
                var assinatura = new Assinatura(
                    idUsuario: novo.Id,
                    idPlano: plano.Id,
                    inicio: DateTime.UtcNow,
                    duracao: plano.Duration.Days,
                    planos: plano
                );

                // Salva a assinatura
                await _assinaturaRepo.AdicionarAsync(assinatura);

                // Atualiza o usuário para definir assinatura atual e a lista de assinaturas
                novo.AssinaturaAtual = assinatura;
                novo.Assinaturas.Add(assinatura);

                // Atualiza o usuário no banco com a referência da assinatura atual
                await _usuarioRepo.AtualizarAsync(novo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }



        public async Task FavoritarMusicaAsync(Guid usuarioId, Guid musicaId)
        {
            var usuario = await _usuarioRepo.ObterPorIdAsync(usuarioId);
            var musica = await _musicaRepo.ObterPorIdAsync(musicaId);

            if (usuario.Favoritos.Any(m => m.Id == musica.Id)) return;

            usuario.Favoritos.Add(musica);
            await _usuarioRepo.AtualizarAsync(usuario);
        }

        public async Task FavoritarBandaAsync(Guid usuarioId, Guid bandaId)
        {
            var usuario = await _usuarioRepo.ObterPorIdAsync(usuarioId);
            var banda = await _bandaRepo.ObterPorIdAsync(bandaId);

            if (usuario.BandasFavoritas.Any(b => b.Id == banda.Id)) return;

            usuario.BandasFavoritas.Add(banda);
            await _usuarioRepo.AtualizarAsync(usuario);
        }

        private string GerarToken(Usuario usuario)
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public async Task<Usuario> Obter(Usuario usuario)
            => await _usuarioRepo.ObterPorIdAsync(usuario.Id) ?? throw new NullReferenceException("Usuário não encontrado.");

        public async Task<Usuario> ObterPorUsername(string username)
            => await _usuarioRepo.ObterPorUsernameAsync(username) ?? throw new NullReferenceException("Usuário não encontrado.");

        public Task<Usuario> ObterPorId(Guid id)
        {
            return _usuarioRepo.ObterPorIdAsync(id);
        }
    }
}

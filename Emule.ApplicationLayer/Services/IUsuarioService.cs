using Emule.ApplicationLayer.DTOs;
using Emule.Domain.Model;

namespace Emule.ApplicationLayer.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> Obter(Usuario usuario);
        Task<Usuario> ObterPorUsername(string username);
        Task<Usuario> ObterPorId(Guid id);
        Task<UsuarioDto?> LoginAsync(string username, string senha);
        Task RegistrarAsync(NovoUsuarioDto novoUsuario);

        Task FavoritarMusicaAsync(Guid usuarioId, Guid musicaId);
        Task FavoritarBandaAsync(Guid usuarioId, Guid musicaId);
    }
}

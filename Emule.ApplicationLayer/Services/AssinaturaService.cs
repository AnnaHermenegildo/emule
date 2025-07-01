using System;
using System.Threading.Tasks;
using Emule.Domain.Model;
using Emule.Domain.Model.Repository;

namespace Emule.ApplicationLayer.Services
{
    public class AssinaturaService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IPlanoRepository _planoRepo;
        private readonly IAssinaturaRepository _assinaturaRepo;

        public AssinaturaService(
            IUsuarioRepository usuarioRepo,
            IPlanoRepository planoRepo,
            IAssinaturaRepository assinaturaRepo)
        {
            _usuarioRepo = usuarioRepo;
            _planoRepo = planoRepo;
            _assinaturaRepo = assinaturaRepo;
        }

        public async Task CriarAssinatura(Guid usuarioId, Guid planoId)
        {
            var usuario = await _usuarioRepo.ObterPorIdAsync(usuarioId);

            if (usuario.AssinaturaAtual != null && usuario.AssinaturaAtual.EstaAtiva())
                throw new Exception("Usuário já possui assinatura ativa.");

            var plano = await _planoRepo.ObterPorIdAsync(planoId);

            var novaAssinatura = new Assinatura(usuarioId, planoId, DateTime.UtcNow, plano!.Duration.Days, plano);

            await _assinaturaRepo.AdicionarAsync(novaAssinatura);
        }


    }
}

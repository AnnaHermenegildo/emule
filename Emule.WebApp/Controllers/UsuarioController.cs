using Emule.ApplicationLayer.DTOs;
using Emule.WebApp.Models;
using Emule.WebApp.Services; // Assumo que UsuarioApiClient e MusicaApiClient estão aqui
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Emule.WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioApiClient _usuarioApiClient;
        private readonly PlanoApiClient _planoApiClient;
        private readonly MusicaApiClient _musicaApiClient; // cliente para músicas

        public UsuarioController(
            UsuarioApiClient usuarioApiClient,
            PlanoApiClient planoApiClient,
            MusicaApiClient musicaApiClient)
        {
            _usuarioApiClient = usuarioApiClient;
            _planoApiClient = planoApiClient;
            _musicaApiClient = musicaApiClient;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var retornoLogin = await _usuarioApiClient.LoginAsync(vm);

            if (retornoLogin is null)
                throw new Exception("Erro ao realizar login.");

            return RedirectToAction("Logado", new {@UsuarioId = retornoLogin.Id.ToString()});
        }

        [HttpGet]
        public async Task<IActionResult> CriarConta()
        {
            var planos = await _planoApiClient.ListarAsync();

            var vm = new NovoUsuarioViewModel
            {
                Planos = planos.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nome
                }).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CriarConta(NovoUsuarioViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var planos = await _planoApiClient.ListarAsync();
                vm.Planos = planos.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nome
                }).ToList();

                return View(vm);
            }

            try
            {
                var novoUsuarioDto = new NovoUsuarioDto
                {
                    Username = vm.Username,
                    Senha = vm.Senha,
                    PlanoId = vm.PlanoSelecionadoId
                };

                await _usuarioApiClient.RegistrarAsync(novoUsuarioDto);
                TempData["Mensagem"] = "Usuário cadastrado com sucesso!";
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Erro ao registrar usuário: " + e.Message);

                var planos = await _planoApiClient.ListarAsync();
                vm.Planos = planos.Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nome
                }).ToList();

                return View(vm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logado(string UsuarioId)
        {

            var usuario = await _usuarioApiClient.Obter(Guid.Parse(UsuarioId));

            // Buscar músicas favoritas
            var musicasFavoritasDto = await _musicaApiClient.ObterFavoritasPorUsuarioAsync(Guid.Parse(UsuarioId));

            // Buscar músicas pela pesquisa
            List<MusicaDto> musicasPesquisaDto = new();
            

            // Mapear para ViewModel
            var musicasFavoritas = musicasFavoritasDto.Select(m => new MusicaViewModel
            {
                Id = m.Id,
                Nome = m.Titulo,
                Favoritada = true
            }).ToList();

            var musicasPesquisa = musicasPesquisaDto.Select(m => new MusicaViewModel
            {
                Id = m.Id,
                Nome = m.Nome,
                Favoritada = musicasFavoritas.Any(f => f.Id == m.Id)
            }).ToList();

            var vm = new UsuarioLogadoViewModel
            {
                NomeUsuario = usuario.Username,
                MusicasFavoritas = musicasFavoritas,
                TermoPesquisa = ""
            };

            ViewBag.MusicasPesquisa = musicasFavoritas;

            // Manter token para a próxima requisição (se usar TempData, reatribua para não perder)
            TempData.Keep("Token");

            return View(vm);
        }

        //[HttpPost]
        //public async Task<IActionResult> ToggleFavorito(Guid musicaId)
        //{


        //    var usuario = await _usuarioApiClient.(token);
        //    if (usuario == null) return Unauthorized();

        //    var foiFavorito = await _musicaApiClient.ToggleFavoritoAsync(usuario.Id, musicaId, token);

        //    // Manter token
        //    TempData.Keep("Token");

        //    return Json(new { sucesso = true, favoritado = foiFavorito });
        //}
    }
}

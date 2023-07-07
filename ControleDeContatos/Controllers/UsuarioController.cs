using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Controllers
{
	public class UsuarioController : Controller
	{
		private readonly IUsuarioRepository _usuarioRepository;

		public UsuarioController(IUsuarioRepository usuarioRepository) 
		{
			_usuarioRepository = usuarioRepository;
		}

		public IActionResult Index()
		{
			var usuarios = _usuarioRepository.Buscartodos();

			return View(usuarios);
		}

		public IActionResult Criar()
		{
			return View();
		}

		public IActionResult Editar(int id)
		{
			var usuario = _usuarioRepository.Buscar(id);
			return View(usuario);
		}


		[HttpPost]
		public IActionResult Criar(UsuarioModel usuario) 
		{
            try
            {
                if (ModelState.IsValid)                     //verifica se o estado da model é valido de acordo com o Data Anotation na própria Model "UsuarioModel"
                {
                    _usuarioRepository.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops! Não foi possível cadastrar usuario, tente novamente, stack trace: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

		public IActionResult Apagar(int id)
		{
			try
			{
				var usuarioDb = _usuarioRepository.Buscar(id);
				_usuarioRepository.Apagar(id);
				TempData["MensagemSucesso"] = $"Usuario {usuarioDb.Nome} excluído com sucesso!";
				return RedirectToAction("Index");
			}
			catch (Exception erro)
			{
				TempData["MensagemErro"] = $"Ops! Não foi possível apagar usuario, tente novamente, stack trace: {erro.Message}";
            }
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Alterar(int id)
		{
			try
			{
                var usuarioDb = _usuarioRepository.Buscar(id);
                if (usuarioDb == null)
                {
					TempData["MensagemErro"] = $"Ops! Nenhum usuário encontrado";
					return RedirectToAction("Index");
                }

				_usuarioRepository.Alterar(usuarioDb);
				return RedirectToAction("Index");
            }
			catch (Exception erro)
			{
                TempData["MensagemErro"] = $"Ops! Não foi possível editar usuario, tente novamente, stack trace: {erro.Message}";
            }

			return RedirectToAction("Index");	
		}
	}
}

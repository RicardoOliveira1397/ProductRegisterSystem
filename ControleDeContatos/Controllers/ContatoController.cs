using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ControleDeContatos.Controllers
{
	public class ContatoController : Controller
	{
		private readonly IContatoRepository _contatoRepository;

		public ContatoController(IContatoRepository contatoRepository)
		{
			_contatoRepository = contatoRepository;
		}
		public IActionResult Index()
		{
			var contatos = _contatoRepository.Buscartodos();

			return View(contatos);
		}

		public IActionResult Criar()
		{
			return View();
		}

		public IActionResult Editar(int id)
		{
			ContatoModel contato = _contatoRepository.BuscarContato(id);

			return View(contato);
		}

		public IActionResult ApagarConfirmacao(int id)
		{
			ContatoModel contato = _contatoRepository.BuscarContato(id);
			
			return View(contato);
		}

		public IActionResult Apagar(int id)
		{
			_contatoRepository.ApagarContato(id);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Criar(ContatoModel contato)
		{
			if (ModelState.IsValid) //verifica se o estado da model é valido de acordo com o Data Anotation na própria Model "ContatoModel"
			{
                _contatoRepository.Adicionar(contato);
				return RedirectToAction("Index");
            }

			return View(contato);
		}

		[HttpPost]
		public IActionResult Alterar(ContatoModel contato)
		{
			_contatoRepository.AlterarContato(contato);

			return RedirectToAction("Index");
		}

	}
}

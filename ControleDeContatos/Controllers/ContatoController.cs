using ControleDeContatos.Models;
using ControleDeContatos.Repository;
using Microsoft.AspNetCore.Mvc;

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
			try
			{
                bool apagado =_contatoRepository.ApagarContato(id);

				if(apagado)
				{
                    TempData["MensagemSucesso"] = "Contato deletado com sucesso!";
                }
				else
				{
                    TempData["MensagemErro"] = $"Ops! Não foi possível apagar contato, tente novamente";
                }

                return RedirectToAction("Index");
            }
			catch (Exception erro)
			{
                TempData["MensagemErro"] = $"Ops! Não foi possível apagar contato, tente novamente, stack trace: {erro.Message}";
                return RedirectToAction("Index");
            }
		}

		[HttpPost]
		public IActionResult Criar(ContatoModel contato)
		{
			try
			{
                if (ModelState.IsValid)                     //verifica se o estado da model é valido de acordo com o Data Anotation na própria Model "ContatoModel"
                {
                    _contatoRepository.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
			catch (Exception erro)
			{
                TempData["MensagemErro"] = $"Ops! Não foi possível cadastrar contato, tente novamente, stack trace: {erro.Message}";
                return RedirectToAction("Index");
            }
		}

		[HttpPost]
		public IActionResult Alterar(ContatoModel contato)
		{
			try
			{
                if (ModelState.IsValid)
                {
                    _contatoRepository.AlterarContato(contato);
                    TempData["MensagemSucesso"] = "Contato alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", contato);
            }
			catch (Exception erro)
			{									
                TempData["MensagemErro"] = $"Ops! Não foi possível atualizar contato, tente novamente, stack trace: {erro.Message}";
                return RedirectToAction("Index");
            }
		}

	}
}

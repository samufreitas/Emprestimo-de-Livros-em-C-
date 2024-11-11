using ClosedXML.Excel;
using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using EmprestimoLivros.Services.EmprestimoService;
using EmprestimoLivros.Services.SessaoService;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        readonly private ApplicationDbContext _bd;
        readonly private ISessaoInterface _sessaoInterface;
        private readonly IEmprestimoInterface _emprestimoInterface;
        public EmprestimoController(ApplicationDbContext db, 
                                    ISessaoInterface sessaoInterface,
                                    IEmprestimoInterface emprestimoInterface)
        {
            _bd = db;
            _sessaoInterface = sessaoInterface;
            _emprestimoInterface = emprestimoInterface;

        }
        public async Task<IActionResult> Index()
        {
            // Exigindo autenticação
            var usuario = _sessaoInterface.BuscarSessao();
            if(usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

           var emprestimos = await _emprestimoInterface.BuscarEmprestimos();
            return View(emprestimos.Dados);
        }

        //Meus métodos de editar um emprestimo
        [HttpGet]
        public async Task<IActionResult> Editar(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            //Pegando o registro com o mesmo id informado no banco de dados
            var emprestimo = await _emprestimoInterface.BuscarEmprestimosPorId(id);

            return View(emprestimo.Dados);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                var emprestimoResult = await _emprestimoInterface.EditarEmprestimo(emprestimo);

                if(emprestimoResult.Status)
                {
                    TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
                }
                else
                {
                    TempData["MensagemErro"] = emprestimoResult.Mensagem;
                    return View(emprestimo);
                }

                return RedirectToAction("Index");
            }
            TempData["MensagemErro"] = "Não foi possível realizar essa operação!";

            return View(emprestimo);
        }


        //Meus métodos de cadastrar um emprestimo
        [HttpGet]
        public IActionResult Cadastrar()

        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(EmprestimosModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                var emprestimoResult = await _emprestimoInterface.CadastrarEmprestimo(emprestimo);

                if (emprestimoResult.Status)
                {
                    TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
                }
                else
                {
                    TempData["MensagemErro"] = emprestimoResult.Mensagem;
                    return View(emprestimoResult);
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        //Método de excluir um emprestimo
        [HttpGet]
        public async Task<IActionResult> Excluir(int? id)
        {
            var usuario = _sessaoInterface.BuscarSessao();
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var emprestimo = await _emprestimoInterface.BuscarEmprestimosPorId(id);

            return View(emprestimo.Dados);
        }
        [HttpPost]
        public async Task<IActionResult> Excluir(EmprestimosModel emprestimo)
        {
            if (emprestimo == null)
            {
                TempData["MensagemErro"] = "Emprestimo não localizado!";
                return View(emprestimo);
            }
            var  emprestimoResult = await _emprestimoInterface.RemoveEmprestimo(emprestimo);
            if(emprestimoResult.Status)
            {
                TempData["MensagemSucesso"] = emprestimoResult.Mensagem;
            }
            else
            {
                TempData["MensagemErro"] = emprestimoResult.Mensagem;
            }
            

            return RedirectToAction("Index");
        }
        // Método de realizar a importação dos dados para um excel 
        public async Task<IActionResult> Exportar()
        {
            var dados = await _emprestimoInterface.BuscarDadosEmprestimoExcel();

            using (XLWorkbook workbook = new XLWorkbook())
            {
                workbook.AddWorksheet(dados, "Dados Empréstimos");

                using (MemoryStream ms = new MemoryStream())
                {
                    workbook.SaveAs(ms);
                    return File(ms.ToArray(), "application/vnd.openxmlformats-officedocument.spredsheetml.sheet", "Emprestimo.xls");
                }
            }
        }

    }
}

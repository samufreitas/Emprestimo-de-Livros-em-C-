using EmprestimoLivros.Models;
using System.Data;

namespace EmprestimoLivros.Services.EmprestimoService
{
    public interface IEmprestimoInterface
    {
        Task<ResponseModel<List<EmprestimosModel>>> BuscarEmprestimos();
        Task<ResponseModel<EmprestimosModel>> BuscarEmprestimosPorId(int? id);
        Task<ResponseModel<EmprestimosModel>> CadastrarEmprestimo(EmprestimosModel emprestimoModel);

        Task<ResponseModel<EmprestimosModel>> EditarEmprestimo(EmprestimosModel emprestimosModel);

        Task<ResponseModel<EmprestimosModel>> RemoveEmprestimo(EmprestimosModel emprestimosModel);

        Task<DataTable> BuscarDadosEmprestimoExcel();

    }
}

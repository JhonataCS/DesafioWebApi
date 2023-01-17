using DesafioWebApi.Model;

namespace DesafioWebApi.Repositorios.Interfaces
{
    public interface IClienteRepositorio
    {
        Task<Response<CotacaoDto>> ObterCotacao(Guid id);
        Task<Response<ResponseClienteCotacao>> ObterClienteComCotacao(Guid id, decimal valorParaCotar);
        Task<ClienteModel> Adicionar(Cadastro cliente);
    }
}

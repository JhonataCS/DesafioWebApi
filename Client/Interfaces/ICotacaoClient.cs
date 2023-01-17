using DesafioWebApi.Model;

namespace DesafioWebApi.Client.Interfaces
{
    public interface ICotacaoClient
    {
        Task<ResponseGenerico<CotacaoModel>> ObterCotacaoDolar();
    }
}

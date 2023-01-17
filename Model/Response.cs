using System.Dynamic;
using System.Net;

namespace DesafioWebApi.Model
{
    public class Response<T> where T : class
    {
        public HttpStatusCode CodigoHttp { get; set; }
        public T DadosRetorno { get; set; }
        public string? Erro { get; set; }
    }
}

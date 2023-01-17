using System.Dynamic;
using System.Net;

namespace DesafioWebApi.Model
{
    public class ResponseGenerico<T> where T : class
    {
        public HttpStatusCode CodigoHttp { get; set; }
        public T DadosRetorno { get; set; }
        public ExpandoObject ErroRetorno { get; set; }
    }
}

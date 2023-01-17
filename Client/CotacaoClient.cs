using DesafioWebApi.Client.Interfaces;
using DesafioWebApi.Model;
using System.Dynamic;
using System.Text.Json;

namespace DesafioWebApi.Client
{
    public class CotacaoClient : ICotacaoClient
    {
        public async Task<ResponseGenerico<CotacaoModel>> ObterCotacaoDolar()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://economia.awesomeapi.com.br/last/USD-BRL");

            var response = new ResponseGenerico<CotacaoModel>();
            using (var client = new HttpClient())
            {
                var responseAwesomeApi = await client.SendAsync(request);
                var contentResp = await responseAwesomeApi.Content.ReadAsStringAsync();
                var objResponse = JsonSerializer.Deserialize<CotacaoModel>(contentResp);

                if (responseAwesomeApi.IsSuccessStatusCode)
                {
                    response.CodigoHttp = responseAwesomeApi.StatusCode;
                    response.DadosRetorno = objResponse;
                }
                else
                {
                    response.CodigoHttp = responseAwesomeApi.StatusCode;
                    response.ErroRetorno = JsonSerializer.Deserialize<ExpandoObject>(contentResp);
                }
            }
            return response;
        }
    }
}

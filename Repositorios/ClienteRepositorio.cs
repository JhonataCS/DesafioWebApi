using DesafioWebApi.Client.Interfaces;
using DesafioWebApi.Data;
using DesafioWebApi.Model;
using DesafioWebApi.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DesafioWebApi.Repositorios
{

    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly ContextoDB _contextoDB;
        private readonly ICotacaoClient _cotacaoClient;
        private readonly string ClienteNaoCadastrado = "Cliente não cadastrado";
        public ClienteRepositorio(ContextoDB contextoDB, ICotacaoClient cotacaoClient)
        {
            _contextoDB = contextoDB;
            _cotacaoClient = cotacaoClient;
        }

        private async Task<ClienteModel> Obter(Guid id)
        {
            return await _contextoDB.Clientes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ClienteModel> ObterPorEmail(string email)
        {
            return await _contextoDB.Clientes.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<ClienteModel> Adicionar(Cadastro cadastro)
        {

            var verificaCadastroExistente = await VerificarClienteExistente(cadastro.Email);

            if (verificaCadastroExistente)
            {
                var cadastroExistente = new ClienteModel
                {
                    Id = null,
                    Nome = cadastro.Nome,
                    Email = cadastro.Email,
                    Multiplicador = cadastro.Multiplicador
                };
                return cadastroExistente;
            }
            var cadastroPreenchido = new ClienteModel
            {
                Id = Guid.NewGuid(),
                Nome = cadastro.Nome,
                Email = cadastro.Email,
                Multiplicador = cadastro.Multiplicador
            };
            await _contextoDB.Clientes.AddAsync(cadastroPreenchido);
            await _contextoDB.SaveChangesAsync();

            return cadastroPreenchido;
        }

        private async Task<bool>VerificarClienteExistente(string email)
        {
            var cliente =  ObterPorEmail(email);

            if (cliente.Result != null)
            {
                return true;
            }

            return false;   
        }

        public async Task<Response<ResponseClienteCotacao>> ObterClienteComCotacao(Guid id, decimal valorParaCotar)
        {
            var response = new Response<ResponseClienteCotacao>();
            var cliente = await Obter(id);

            if (cliente == null)
            {
                response.CodigoHttp = HttpStatusCode.BadRequest;
                response.Erro = ClienteNaoCadastrado;

                return response;
            }
            var cotacao = await _cotacaoClient.ObterCotacaoDolar();

            var cotacaoResul = cotacao.DadosRetorno.USDBRL;

            var valorOriginal= valorParaCotar / Convert.ToDecimal(cotacaoResul.Bid);

            var valorComTaxa = cliente.Multiplicador * valorOriginal;

            var clienteResul = new ClienteDto
            {
                Id = cliente.Id,
                Email = cliente.Email,
                Nome = cliente.Nome,
            };

            var clienteCotacao = new ResponseClienteCotacao
            {
                Cliente = clienteResul,
                ValorCotadoEmReais = valorParaCotar,
                ValorOriginal = valorOriginal,
                ValorComTaxa = valorComTaxa
            };

            response.CodigoHttp = HttpStatusCode.OK;
            response.DadosRetorno = clienteCotacao;
            

            return response;
        }

        public async Task<Response<CotacaoDto>> ObterCotacao(Guid id)
        {
            var response = new Response<CotacaoDto>();
            var cliente = await Obter(id);

            if (cliente == null)
            {
                response.CodigoHttp = HttpStatusCode.BadRequest;
                response.Erro = ClienteNaoCadastrado;

                return response;
            }
            var cotacao = await _cotacaoClient.ObterCotacaoDolar();

            var cotacaoResul = cotacao.DadosRetorno.USDBRL;

            var valorOriginal= Convert.ToDecimal(cotacaoResul.VarBid);

            var valorComTaxa = cliente.Multiplicador * Convert.ToDecimal(cotacaoResul.Bid);

            var clienteCotacao = new CotacaoDto
            {
                ValorOriginal = valorOriginal,
                ValorComTaxa = valorComTaxa

            };

            response.CodigoHttp = HttpStatusCode.OK;
            response.DadosRetorno = clienteCotacao;


            return response;
        }

    }
}

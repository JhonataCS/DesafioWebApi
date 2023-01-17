using DesafioWebApi.Model;
using DesafioWebApi.Repositorios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DesafioWebApi.Controllers
{
    [Route("api/v1/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }
        [HttpPost]
        public async Task<ActionResult<ClienteModel>> CadastrarUsuario(
            [FromBody] Cadastro clienteModel)
        {
            ClienteModel response = await _clienteRepositorio.Adicionar(clienteModel);
            if (response.Id == null)
            {
                return Ok(response.Email);
            }
            return Ok(response.Id);
        }

        [HttpGet("{id}/cotacao")]
        public async Task<ActionResult<Response<CotacaoDto>>> ObterCotacao(
            Guid id)
        {
            Response<CotacaoDto> response = await _clienteRepositorio.ObterCotacao(id);
            if (response.DadosRetorno == null)
            {
                return Ok(response.Erro);
            }
            return Ok(response);
        }

        [HttpPatch("{id}/cotacao")]
        public async Task<ActionResult<Response<ResponseClienteCotacao>>> ObterClienteComCotacao(
            Guid id, 
            [FromBody]ObterCotacaoRequest obterCotacaoRequest )
        {
            Response<ResponseClienteCotacao> response = await _clienteRepositorio.ObterClienteComCotacao
                                                        (id, obterCotacaoRequest.valorCotadoEmReais);
            if (response.CodigoHttp != HttpStatusCode.OK)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }


    }
}

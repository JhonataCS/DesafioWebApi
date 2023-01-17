namespace DesafioWebApi.Model
{
    public class ResponseClienteCotacao
    {
        public ClienteDto Cliente { get; set; }
        public decimal ValorCotadoEmReais { get; set; }
        public decimal ValorOriginal { get; set; }
        public decimal ValorComTaxa { get; set; }
    }
}

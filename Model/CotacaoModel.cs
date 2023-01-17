using System.Text.Json.Serialization;

namespace DesafioWebApi.Model
{

    public class CotacaoModel
    {
        public USDBRL USDBRL { get; set; }
    }



    public class USDBRL
    {
        [JsonPropertyName("code")]
        public string Codigo { get; set; }



        [JsonPropertyName("codein")]
        public string CodigoEm { get; set; }



        [JsonPropertyName("name")]
        public string Nome { get; set; }



        [JsonPropertyName("high")]
        public string ValorAlto { get; set; }



        [JsonPropertyName("low")]
        public string ValorBaixo { get; set; }



        [JsonPropertyName("varBid")]
        public string VarBid { get; set; }



        [JsonPropertyName("pctChange")]
        public string PctChange { get; set; }



        [JsonPropertyName("bid")]
        public string Bid { get; set; }



        [JsonPropertyName("ask")]
        public string Ask { get; set; }



        [JsonPropertyName("timestamp")]
        public string Timestamp { get; set; }



        [JsonPropertyName("create_date")]
        public string DataCriacao { get; set; }
    }
}


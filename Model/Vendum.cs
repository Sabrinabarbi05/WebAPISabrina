using System.Text.Json.Serialization;

namespace WebApiBia.Model
{
    public class Vendum
    {
        public int Id { get; set; }

        public decimal Valor { get; set; }
        [JsonIgnore]
        public byte[]? NotaFiscal { get; set; }

        public int FkProduto { get; set; }

        public int FkCliente { get; set; }

        [JsonIgnore]
        public string? NotaFiscalBase64 => NotaFiscal != null ? Convert.ToBase64String(NotaFiscal) : null;

        public string UrlNotaFiscal { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}

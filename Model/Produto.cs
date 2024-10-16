using System.Text.Json.Serialization;

namespace WebApiBia.Model
{
    public class Produto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;

        public decimal Preco { get; set; }

        public int Quantidade { get; set; }
        [JsonIgnore]
        public byte[]? NotaFiscal { get; set; } = null!;
        [JsonIgnore]
        public string? NotaFiscalBase64 => NotaFiscal != null ? Convert.ToBase64String(NotaFiscal) : null;

        public string UrlNotaFiscal { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}

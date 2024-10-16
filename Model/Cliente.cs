using System.Text.Json.Serialization;

namespace WebApiBia.Model
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nome { get; set; } = null!;
        [JsonIgnore] 
        public byte[]? DocIdentificacao { get; set; }

        public string Telefone { get; set; } = null!;

      
        [JsonIgnore] 
        public string? DocIdentificacaoBase64 => DocIdentificacao != null ? Convert.ToBase64String(DocIdentificacao) : null;

        public string UrlDocIdentificacao { get; set; } // Certifique-se de que esta propriedade esteja visível
    }
}

using System.Text.Json.Serialization;

namespace WebApiBia.Model
{
    public class VendumDto
    {
        public decimal Valor { get; set; }
        
        public IFormFile NotaFiscal { get; set; }

        public int FkProduto { get; set; }

        public int FkCliente { get; set; }

    }
}

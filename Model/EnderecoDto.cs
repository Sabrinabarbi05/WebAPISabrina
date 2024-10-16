namespace WebApiBia.Model
{
    public class EnderecoDto
    {
      
        public string Logradouro { get; set; } = null!;

        public string Cidade { get; set; } = null!;

        public string Estado { get; set; } = null!;

        public string Cep { get; set; } = null!;

        public string PontoReferencia { get; set; } = null!;

        public int Numero { get; set; }

        public int FkCliente { get; set; }

    }
}

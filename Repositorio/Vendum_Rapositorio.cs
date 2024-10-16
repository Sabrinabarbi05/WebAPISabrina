using WebApiBia.Model;
using WebApiBia.ORM;

namespace WebApiBia.Repositorio
{
    public class Vendum_Repositorio
    {
        private ProjetoBancoContext _context;

        public Vendum_Repositorio(ProjetoBancoContext context)
        {
            _context = context;
        }

        public void Add(Vendum vendum, IFormFile NotaFiscal)
        {
            // Verifica se uma foto foi enviada
            byte[] NotaFiscalBytes = null;
            if (NotaFiscal != null && NotaFiscal.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    NotaFiscal.CopyTo(memoryStream);
                    NotaFiscalBytes = memoryStream.ToArray();
                }
            }

            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbVendum = new TbVendum()
            {
                Valor = vendum.Valor,
                FkProduto = vendum.FkProduto,
                FkCliente = vendum.FkCliente,
                NotaFiscal = NotaFiscalBytes // Armazena a foto na entidade
            };

            // Adiciona a entidade ao contexto
            _context.TbVenda.Add(tbVendum);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVendum = _context.TbVenda.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbVendum != null)
            {
                // Remove a entidade do contexto
                _context.TbVenda.Remove(tbVendum);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrada.");
            }
        }

        public List<Vendum> GetAll()
        {
            List<Vendum> listFun = new List<Vendum>();

            var listTb = _context.TbVenda.ToList();

            foreach (var item in listTb)
            {
                var vendum = new Vendum
                {
                    Id = item.Id,                   
                    Valor = item.Valor,
                    FkProduto = item.FkProduto,
                    FkCliente = item.FkCliente,
                };

                listFun.Add(vendum);
            }

            return listFun;
        }

        public Vendum GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbVenda.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var vendum = new Vendum
            {
                Id = item.Id,
                Valor = item.Valor,
                FkProduto = item.FkProduto,
                FkCliente = item.FkCliente,
                NotaFiscal = item.NotaFiscal // Mantém o campo Foto como byte[]
            };
            return vendum; // Retorna o funcionário encontrado
        }

        public void Update(Vendum vendum, IFormFile NotaFiscal)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbVendum = _context.TbVenda.FirstOrDefault(f => f.Id == vendum.Id);

            // Verifica se a entidade foi encontrada
            if (tbVendum != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbVendum.Valor = vendum.Valor;
                tbVendum.FkProduto = vendum.FkProduto;
                tbVendum.FkCliente = vendum.FkCliente;

                // Verifica se uma nova foto foi enviada
                if (NotaFiscal != null && NotaFiscal.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        NotaFiscal.CopyTo(memoryStream);
                        tbVendum.NotaFiscal = memoryStream.ToArray(); // Atualiza a foto na entidade
                    }
                }

                // Atualiza as informações no contexto
                _context.TbVenda.Update(tbVendum);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Venda não encontrada.");
            }
        }
    }
}

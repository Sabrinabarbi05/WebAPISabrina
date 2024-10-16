using WebApiBia.Model;
using WebApiBia.ORM;

namespace WebApiBia.Repositorio
{
    public class Endereco_Repositorio
    {
        private ProjetoBancoContext _context;

        public Endereco_Repositorio(ProjetoBancoContext context)
        {
            _context = context;
        }

        public void Add(Endereco endereco)
        {
                       
            // Cria uma nova entidade do tipo TbFuncionario a partir do objeto Funcionario recebido
            var tbEndereco = new TbEndereco()
            {
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                Numero = endereco.Numero,
                FkCliente = endereco.FkCliente,
               
            };

            // Adiciona a entidade ao contexto
            _context.TbEnderecos.Add(tbEndereco);

            // Salva as mudanças no banco de dados
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Remove a entidade do contexto
                _context.TbEnderecos.Remove(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }

        public List<Endereco> GetAll()
        {
            List<Endereco> listFun = new List<Endereco>();

            var listTb = _context.TbEnderecos.ToList();

            foreach (var item in listTb)
            {
                var endereco = new Endereco
                {
                    Id = item.Id,
                    Logradouro = item.Logradouro,
                    Cidade = item.Cidade,
                    Estado = item.Estado,
                    Cep = item.Cep,
                    PontoReferencia = item.PontoReferencia,
                    Numero = item.Numero,
                    FkCliente = item.FkCliente,
                };

                listFun.Add(endereco);
            }

            return listFun;
        }

        public Endereco GetById(int id)
        {
            // Busca o funcionário pelo ID no banco de dados
            var item = _context.TbEnderecos.FirstOrDefault(f => f.Id == id);

            // Verifica se o funcionário foi encontrado
            if (item == null)
            {
                return null; // Retorna null se não encontrar
            }

            // Mapeia o objeto encontrado para a classe Funcionario
            var endereco = new Endereco
            {
                Id = item.Id,
                Logradouro = item.Logradouro,
                Cidade = item.Cidade,
                Estado = item.Estado,
                Cep = item.Cep,
                PontoReferencia = item.PontoReferencia,
                Numero = item.Numero,
                FkCliente = item.FkCliente,
                
            };
            return endereco; // Retorna o funcionário encontrado
        }

        public void Update(Endereco endereco)
        {
            // Busca a entidade existente no banco de dados pelo Id
            var tbEndereco = _context.TbEnderecos.FirstOrDefault(f => f.Id == endereco.Id);

            // Verifica se a entidade foi encontrada
            if (tbEndereco != null)
            {
                // Atualiza os campos da entidade com os valores do objeto Funcionario recebido
                tbEndereco.Logradouro = endereco.Logradouro;
                tbEndereco.Cidade = endereco.Cidade;
                tbEndereco.Estado = endereco.Estado;
                tbEndereco.Cep = endereco.Cep;
                tbEndereco.PontoReferencia = endereco.PontoReferencia;
                tbEndereco.Numero = endereco.Numero;
                tbEndereco.FkCliente = endereco.FkCliente;

               
                // Atualiza as informações no contexto
                _context.TbEnderecos.Update(tbEndereco);

                // Salva as mudanças no banco de dados
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Endereço não encontrado.");
            }
        }
    }
}

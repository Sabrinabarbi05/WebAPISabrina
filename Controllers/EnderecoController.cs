using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiBia.Model;
using WebApiBia.Repositorio;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiBia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EnderecoController : ControllerBase
    {
        private readonly Endereco_Repositorio _enderecoRepo; // O repositório que contém GetAll()

        public EnderecoController(Endereco_Repositorio enderecoRepo)
        {
            _enderecoRepo = enderecoRepo;
        }

        // GET: api/Endereco
        [HttpGet]
        public ActionResult<List<Endereco>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var enderecos = _enderecoRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (enderecos == null || !enderecos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum endereço encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = enderecos.Select(endereco => new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                Numero = endereco.Numero,
                FkCliente = endereco.FkCliente,
               
               
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Endereco> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var endereco = _enderecoRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (endereco == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var enderecoComUrl = new Endereco
            {
                Id = endereco.Id,
                Logradouro = endereco.Logradouro,
                Cidade = endereco.Cidade,
                Estado = endereco.Estado,
                Cep = endereco.Cep,
                PontoReferencia = endereco.PontoReferencia,
                Numero = endereco.Numero,
                FkCliente = endereco.FkCliente,
               
            };

            // Retorna o funcionário com status 200 OK
            return Ok(enderecoComUrl);
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] EnderecoDto novoEndereco)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var endereco = new Endereco
            {
                Logradouro = novoEndereco.Logradouro,
                Cidade = novoEndereco.Cidade,
                Estado = novoEndereco.Estado,
                Cep = novoEndereco.Cep,
                PontoReferencia = novoEndereco.PontoReferencia,
                Numero = novoEndereco.Numero,
                FkCliente = novoEndereco.FkCliente,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _enderecoRepo.Add(endereco);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço cadastrado com sucesso!",
                Logradouro = novoEndereco.Logradouro,
                Cidade = novoEndereco.Cidade,
                Estado = novoEndereco.Estado,
                Cep = novoEndereco.Cep,
                PontoReferencia = novoEndereco.PontoReferencia,
                Numero = novoEndereco.Numero,
                FkCliente = novoEndereco.FkCliente,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] EnderecoDto enderecoAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            enderecoExistente.Logradouro = enderecoAtualizado.Logradouro;
            enderecoExistente.Cidade = enderecoAtualizado.Cidade;
            enderecoExistente.Estado = enderecoAtualizado.Estado;
            enderecoExistente.Cep = enderecoAtualizado.Cep;

            // Chama o método de atualização do repositório, passando a nova foto
            _enderecoRepo.Update(enderecoExistente);

           

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço atualizado com sucesso!",
                
                Logradouro = enderecoExistente.Logradouro,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                PontoReferencia = enderecoExistente.PontoReferencia,
                Numero = enderecoExistente.Numero,
                FkCliente = enderecoExistente.FkCliente,

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var enderecoExistente = _enderecoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (enderecoExistente == null)
            {
                return NotFound(new { Mensagem = "Endereço não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _enderecoRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Endereço excluído com sucesso!",
                Logradouro = enderecoExistente.Logradouro,
                Cidade = enderecoExistente.Cidade,
                Estado = enderecoExistente.Estado,
                Cep = enderecoExistente.Cep,
                PontoReferencia = enderecoExistente.PontoReferencia,
                Numero = enderecoExistente.Numero,
                FkCliente = enderecoExistente.FkCliente,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}

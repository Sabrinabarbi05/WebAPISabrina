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
    public class VendumController : ControllerBase
    {
        private readonly Vendum_Repositorio _vendumRepo; // O repositório que contém GetAll()

        public VendumController(Vendum_Repositorio vendumRepo)
        {
            _vendumRepo = vendumRepo;
        }

        // GET: api/Funcionario/{id}/foto
        [HttpGet("{id}/NotaFiscal")]
        public IActionResult GetDocIdentificacao(int id)
        {
            // Busca o funcionário pelo ID
            var vendum = _vendumRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendum == null || vendum.NotaFiscal == null)
            {
                return NotFound(new { Mensagem = "Nota Fiscal não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(vendum.NotaFiscal, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Vendum>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var venda = _vendumRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (venda == null || !venda.Any())
            {
                return NotFound(new { Mensagem = "Nenhuma venda encontrada." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = venda.Select(vendum => new Vendum
            {
                Id = vendum.Id,
                Valor = vendum.Valor,
                FkProduto = vendum.FkProduto,
                FkCliente = vendum.FkCliente,
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Vendum/{vendum.Id}/NotaFiscal" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Vendum> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var vendum = _vendumRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (vendum == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var vendumComUrl = new Vendum
            {
                Id = vendum.Id,
                Valor = vendum.Valor,
                FkProduto = vendum.FkProduto,
                FkCliente = vendum.FkCliente,
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Vendum/{vendum.Id}/NotaFiscal" // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(vendumComUrl);
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] VendumDto novoVendum)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var vendum = new Vendum
            {
                Valor = novoVendum.Valor,
                FkProduto = novoVendum.FkProduto,
                FkCliente = novoVendum.FkCliente,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _vendumRepo.Add(vendum, novoVendum.NotaFiscal);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Venda cadastrada com sucesso!",
                Valor = vendum.Valor,
                FkProduto = vendum.FkProduto,
                FkCliente = vendum.FkCliente,
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] VendumDto vendumAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var vendumExistente = _vendumRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendumExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            vendumExistente.Valor = vendumAtualizado.Valor;
            vendumExistente.FkProduto = vendumAtualizado.FkProduto;
            vendumExistente.FkCliente = vendumAtualizado.FkCliente;

            // Chama o método de atualização do repositório, passando a nova foto
            _vendumRepo.Update(vendumExistente, vendumAtualizado.NotaFiscal);

            // Cria a URL da foto
            var urlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Vendum/{vendumExistente.Id}/NotaFiscal";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Usuário atualizado com sucesso!",
                Valor = vendumExistente.Valor,
                FkProduto = vendumExistente.FkProduto,
                FkCliente = vendumExistente.FkCliente,
                UrlNotaFiscal = urlNotaFiscal // Inclui a URL da foto na resposta
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // DELETE api/<FuncionarioController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            // Busca o funcionário existente pelo Id
            var vendumExistente = _vendumRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (vendumExistente == null)
            {
                return NotFound(new { Mensagem = "Venda não encontrada." });
            }

            // Chama o método de exclusão do repositório
            _vendumRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Venda excluída com sucesso!",
                Valor = vendumExistente.Valor,
                FkProduto = vendumExistente.FkProduto,
                FkCliente = vendumExistente.FkCliente
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }
    }
}

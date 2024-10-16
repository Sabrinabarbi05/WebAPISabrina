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
    public class ProdutoController : ControllerBase
    {
        private readonly Produto_Repositorio _produtoRepo; // O repositório que contém GetAll()

        public ProdutoController(Produto_Repositorio produtoRepo)
        {
            _produtoRepo = produtoRepo;
        }

        // GET: api/Funcionario/{id}/foto
        [HttpGet("{id}/NotaFiscal")]
        public IActionResult GetNotaFiscal(int id)
        {
            // Busca o funcionário pelo ID
            var produto = _produtoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produto == null || produto.NotaFiscal == null)
            {
                return NotFound(new { Mensagem = "Nota Fiscal não encontrada." });
            }

            // Retorna a foto como um arquivo de imagem
            return File(produto.NotaFiscal, "image/jpeg"); // Ou "image/png" dependendo do formato
        }

        // GET: api/Funcionario
        [HttpGet]
        public ActionResult<List<Produto>> GetAll()
        {
            // Chama o repositório para obter todos os funcionários
            var produtos = _produtoRepo.GetAll();

            // Verifica se a lista de funcionários está vazia
            if (produtos == null || !produtos.Any())
            {
                return NotFound(new { Mensagem = "Nenhum produto encontrado." });
            }

            // Mapeia a lista de funcionários para incluir a URL da foto
            var listaComUrl = produtos.Select(produto => new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{produto.Id}/NotaFiscal" // Define a URL completa para a imagem
            }).ToList();

            // Retorna a lista de funcionários com status 200 OK
            return Ok(listaComUrl);
        }

        // GET: api/Funcionario/{id}
        [HttpGet("{id}")]
        public ActionResult<Produto> GetById(int id)
        {
            // Chama o repositório para obter o funcionário pelo ID
            var produto = _produtoRepo.GetById(id);

            // Se o funcionário não for encontrado, retorna uma resposta 404
            if (produto == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." }); // Retorna 404 com mensagem
            }

            // Mapeia o funcionário encontrado para incluir a URL da foto
            var produtoComUrl = new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                UrlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{produto.Id}/NotaFiscal" // Define a URL completa para a imagem
            };

            // Retorna o funcionário com status 200 OK
            return Ok(produtoComUrl);
        }

        // POST api/<FuncionarioController>        
        [HttpPost]
        public ActionResult<object> Post([FromForm] ProdutoDto novoProduto)
        {
            // Cria uma nova instância do modelo Funcionario a partir do DTO recebido
            var produto = new Produto
            {
                Nome = novoProduto.Nome,
                Preco = novoProduto.Preco,
                Quantidade = novoProduto.Quantidade,
            };

            // Chama o método de adicionar do repositório, passando a foto como parâmetro
            _produtoRepo.Add(produto, novoProduto.NotaFiscal);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto cadastrado com sucesso!",
                Nome = produto.Nome,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                
            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

        // PUT api/<FuncionarioController>        
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm] ProdutoDto produtoAtualizado)
        {
            // Busca o funcionário existente pelo Id
            var produtoExistente = _produtoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Atualiza os dados do funcionário existente com os valores do objeto recebido
            produtoExistente.Nome = produtoAtualizado.Nome;
            produtoExistente.Preco = produtoAtualizado.Preco;
            produtoExistente.Quantidade = produtoAtualizado.Quantidade;
           
            // Chama o método de atualização do repositório, passando a nova foto
            _produtoRepo.Update(produtoExistente, produtoAtualizado.NotaFiscal);

            // Cria a URL da foto
            var  urlNotaFiscal = $"{Request.Scheme}://{Request.Host}/api/Produto/{produtoExistente.Id}/NotaFiscal";

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto atualizado com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
                Quantidade = produtoExistente.Quantidade,
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
            var produtoExistente = _produtoRepo.GetById(id);

            // Verifica se o funcionário foi encontrado
            if (produtoExistente == null)
            {
                return NotFound(new { Mensagem = "Produto não encontrado." });
            }

            // Chama o método de exclusão do repositório
            _produtoRepo.Delete(id);

            // Cria um objeto anônimo para retornar
            var resultado = new
            {
                Mensagem = "Produto excluído com sucesso!",
                Nome = produtoExistente.Nome,
                Preco = produtoExistente.Preco,
                Quantidade=produtoExistente.Quantidade, 
                
                

            };

            // Retorna o objeto com status 200 OK
            return Ok(resultado);
        }

    }


}

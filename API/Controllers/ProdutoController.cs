using System;
using System.Linq;
using API.Data;
using API.DTO;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]    
    public class ProdutoController : ControllerBase
    {
        private readonly ApplicationDbContext _database;
        private readonly FileHelper _fileHelper;        
        
        public ProdutoController(ApplicationDbContext database, IWebHostEnvironment hostEnvironment)
        {
            _database = database;
            _fileHelper = new FileHelper(hostEnvironment);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult MostrarTodos()
        {
            var produtosBd = _database.Produtos.Where(produto => produto.Ativo == true).ToList();
            return Ok(produtosBd.ToArray());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult MostrarUm(int id)
        {
            try
            {
                var produto = _database.Produtos.Where(produto => produto.Ativo == true).First(produto => produto.Id == id);
                return Ok(produto);
            }
            catch (System.Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Produto não encontrado");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult NovoProduto([FromForm] ProdutoDto modeloProduto)
        {
            if(ModelState.IsValid)
            {
                var nomeFoto = _fileHelper.UploadedFile(modeloProduto.Imagem, modeloProduto.Marca);

                Produto novoProduto = new Produto();
                novoProduto.Marca = modeloProduto.Marca;
                novoProduto.Descricao = modeloProduto.Descricao;
                novoProduto.UnidadeDeMedida = modeloProduto.UnidadeDeMedida;
                novoProduto.QuantidadeEstoque = modeloProduto.QuantidadeEstoque;
                novoProduto.PrecoUnitario = modeloProduto.PrecoUnitario;
                novoProduto.Foto = nomeFoto;
                novoProduto.Ativo = true;

                _database.Produtos.Add(novoProduto);
                _database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult(novoProduto);
            }
            return BadRequest();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditarProduto(int id, [FromForm] ProdutoPatchDto modeloProduto)
        {
            try
            {
                var produtoBd = _database.Produtos.First(produto => produto.Id == id);

                produtoBd.Marca = string.IsNullOrWhiteSpace(modeloProduto.Marca) ? produtoBd.Marca : modeloProduto.Marca;
                produtoBd.Descricao = string.IsNullOrWhiteSpace(modeloProduto.Descricao) ? produtoBd.Descricao : modeloProduto.Descricao;
                produtoBd.UnidadeDeMedida = string.IsNullOrWhiteSpace(modeloProduto.UnidadeDeMedida) ? produtoBd.UnidadeDeMedida : modeloProduto.UnidadeDeMedida;
                produtoBd.QuantidadeEstoque = modeloProduto.QuantidadeEstoque < 0 ? produtoBd.QuantidadeEstoque : modeloProduto.QuantidadeEstoque;
                produtoBd.PrecoUnitario = modeloProduto.PrecoUnitario < 0 ? produtoBd.PrecoUnitario : modeloProduto.PrecoUnitario;
                produtoBd.Foto = modeloProduto.Imagem == null ? produtoBd.Foto : _fileHelper.UploadedFile(modeloProduto.Imagem, modeloProduto.Descricao);
                produtoBd.Ativo = true;

                _database.SaveChanges();

                return Ok(produtoBd);

            }
            catch (System.Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Produto não encontrado");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeletarProduto(int id)
        {
            try
            {
                var produtoBd = _database.Produtos.First(produto => produto.Id == id);

                produtoBd.Ativo = false;

                _database.SaveChanges();
                return Ok(new {msg = "Produto excluído", produtoBd});
            }
            catch (System.Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Produto não encontrado");
            }
        }        
    }
}
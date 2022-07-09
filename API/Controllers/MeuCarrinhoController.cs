using System;
using System.Linq;
using API.Data;
using API.DTO;
using API.Enums;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Comum")]    
    public class MeuCarrinhoController : ControllerBase
    {
        private readonly ApplicationDbContext _database;

        public MeuCarrinhoController(ApplicationDbContext database)
        {
            _database = database;
        }

        [HttpGet("Itens")]
        public IActionResult PegarItens()
        {
            var idUsuario = PegarIdUsuarioLogado();

            try
            {
                var carrinhoAberto = _database.Carrinhos.Where(carrinho => carrinho.Status == EStatusCarrinho.Aberto).First(carrinho => carrinho.IdCliente == idUsuario);
                var itensCarrinho = _database.Itens.Where(item => item.CarrinhoId == carrinhoAberto.Id).Include(produto => produto.Produto).ToList();

                return Ok(itensCarrinho.ToArray());
            }
            catch (System.Exception)
            {
                return BadRequest("Não há carrinho aberto");
            }
           
        }

        [HttpGet]
        public IActionResult PegarCarrinho()
        {
            var idUsuario = PegarIdUsuarioLogado();

            try
            {
                var carrinhoAberto = _database.Carrinhos.Where(carrinho => carrinho.Status == EStatusCarrinho.Aberto).First(carrinho => carrinho.IdCliente == idUsuario);
                return Ok(carrinhoAberto);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }           
        }        

        [HttpGet("Todos")]
        public IActionResult PegarCarrinhos()
        {
            var idUsuario = PegarIdUsuarioLogado();

            try
            {
                var carrinhosFechados = _database.Carrinhos.Where(carrinho => carrinho.Status == EStatusCarrinho.Fechado).Where(carrinho => carrinho.IdCliente == idUsuario).ToList();
                return Ok(carrinhosFechados.ToArray());
            }
            catch (System.Exception)
            {
                return BadRequest();
            } 
        }

        [HttpPost("AdicionarProduto/{id}")]
        public IActionResult AdicionarItem(int id, [FromBody] ItemCarrinhoDto modelo)
        {
            if(ModelState.IsValid)
            {
                var produtoBd = _database.Produtos.FirstOrDefault(produto => produto.Id == id);

                if(produtoBd == null)
                {
                    Response.StatusCode = 404;
                    return new ObjectResult("Produto não encontrado");
                }
                var idUsuario = PegarIdUsuarioLogado();

                var carrinhoAberto = _database.Carrinhos.Where(carrinho => carrinho.Status == Enums.EStatusCarrinho.Aberto).FirstOrDefault(carrinho => carrinho.IdCliente == idUsuario);
            
                if(carrinhoAberto == null)
                {
                    carrinhoAberto = new CarrinhoDeCompra();
                    carrinhoAberto.IdCliente = idUsuario;
                    carrinhoAberto.Status = Enums.EStatusCarrinho.Aberto;
                    _database.Carrinhos.Add(carrinhoAberto);
                    _database.SaveChanges();
                }

                var novoItem = new ItemCompra() 
                {
                    CarrinhoId = carrinhoAberto.Id,
                    ProdutoId = produtoBd.Id,
                    QuantidadeProduto = (produtoBd.QuantidadeEstoque - modelo.Quantidade) >= 0 ? modelo.Quantidade : produtoBd.QuantidadeEstoque,
                };
                novoItem.ValorTotalItem = novoItem.QuantidadeProduto * produtoBd.PrecoUnitario;
                carrinhoAberto.TotalVenda += novoItem.ValorTotalItem;

                produtoBd.RetirarEstoque(novoItem.QuantidadeProduto);

                _database.Itens.Add(novoItem);
                _database.SaveChanges();

                return Ok("Item adicionado com sucesso!");
            }
            return BadRequest("Quantidade não é válida");            
        }

        [HttpDelete("ExcluirProduto/{id}")]
        public IActionResult DeletarItem(int id)
        {
            var idUsuario = PegarIdUsuarioLogado();
            var carrinhoAberto = _database.Carrinhos.Where(carrinho => carrinho.Status == Enums.EStatusCarrinho.Aberto).FirstOrDefault(carrinho => carrinho.IdCliente == idUsuario);

            try
            {
                var itemBd = _database.Itens.Where(produto => produto.CarrinhoId == carrinhoAberto.Id).First(produto => produto.Id == id);
                carrinhoAberto.TotalVenda -= itemBd.ValorTotalItem;
                
                var produtoBd = _database.Produtos.First(produto => produto.Id == itemBd.ProdutoId);
                produtoBd.AdicionarEstoque(itemBd.QuantidadeProduto);
                
                _database.Remove(itemBd);
                _database.SaveChanges();

                return Ok("Item excluído do carrinho");
            }
            catch (System.Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Item do carrinho não encontrado");
            }
        }

        [HttpPost("Comprar")]
        public IActionResult FecharCompra([FromBody] EnderecoDto modeloEndereco)
        {
            var idUsuario = PegarIdUsuarioLogado();

            try
            {
                var carrinhoAberto = _database.Carrinhos.Where(carrinho => carrinho.Status == Enums.EStatusCarrinho.Aberto).First(carrinho => carrinho.IdCliente == idUsuario);
                carrinhoAberto.DataVenda = DateTime.Now;
                carrinhoAberto.CepEnvio = modeloEndereco.Cep;
                carrinhoAberto.Estado = modeloEndereco.State; 
                carrinhoAberto.Cidade = modeloEndereco.City;
                carrinhoAberto.Bairro = modeloEndereco.Neighborhood;
                carrinhoAberto.Logradouro = modeloEndereco.Street;
                carrinhoAberto.NumeroImovel = modeloEndereco.Number;
                carrinhoAberto.Status = EStatusCarrinho.Fechado;

                _database.SaveChanges();
                return Ok("Compra realizada com sucesso!");
            }
            catch (System.Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult("Não há carrinho em aberto");
            }
        }
        private string PegarIdUsuarioLogado()
        {
            return HttpContext.User.Claims.First(claim => claim.Type.ToString().Equals("id", System.StringComparison.InvariantCultureIgnoreCase)).Value;
        }
    }
}
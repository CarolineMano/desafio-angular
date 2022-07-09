namespace API.Models
{
    public class ItemCompra
    {
        public int Id { get; set; }
        public int CarrinhoId { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int QuantidadeProduto { get; set; }
        public float ValorTotalItem { get; set; }
    }
}
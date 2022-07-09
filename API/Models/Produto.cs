namespace API.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Descricao { get; set; }
        public string UnidadeDeMedida { get; set; }
        public string Foto { get; set; }
        public float PrecoUnitario { get; set; }
        public int QuantidadeEstoque { get; set; }
        public bool Ativo { get; set; }

        public void AdicionarEstoque(int entrada)
        {
            QuantidadeEstoque += entrada;
        }

        public void RetirarEstoque(int saida)
        {
            QuantidadeEstoque -= saida;
        }
    }
}
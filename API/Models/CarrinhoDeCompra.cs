using System;
using API.Enums;

namespace API.Models
{
    public class CarrinhoDeCompra
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public string IdCliente { get; set; }
        public float TotalVenda { get; set; }
        public string CepEnvio { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string NumeroImovel { get; set; }
        public EStatusCarrinho Status { get; set; }
    }
}
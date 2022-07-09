using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace API.DTO
{
    public class ProdutoPatchDto
    {
        [StringLength(50, ErrorMessage = "O nome da marca deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Marca { get; set; }
        [StringLength(100, ErrorMessage = "A descrição deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }
        [StringLength(30, ErrorMessage = "A unidade de medida deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string UnidadeDeMedida { get; set; }
        [Range(0.0, float.MaxValue, ErrorMessage = "O valor unitário deve ser maior ou igual a {1}.")]
        public float PrecoUnitario { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Valor mínimo para o estoque é {1}")]
        public int QuantidadeEstoque { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class ItemCarrinhoDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantidade mínima de produtos é {1}")]        
        public int Quantidade { get; set; }
    }
}
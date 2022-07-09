using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class LoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]        
        public string Email { get; set; }
        [Required]        
        public string Senha { get; set; }
    }
}
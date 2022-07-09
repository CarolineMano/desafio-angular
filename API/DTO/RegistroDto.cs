using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegistroDto
    {
        [Required]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter {1} dígitos")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "O CPF só pode conter números")]
        public string Cpf { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "O nome deve conter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)] 
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Deve ser um email no formato válido")]       
        public string Email { get; set; }
        [Required]        
        [DataType(DataType.Password)]
        public string Senha { get; set; }
        [Required]        
        [DataType(DataType.Password)]
        [Compare("Senha")]        
        public string ConfirmacaoSenha { get; set; }
    }
}
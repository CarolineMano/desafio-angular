using Microsoft.AspNetCore.Identity;

namespace API.Models
{
    public class Usuario : IdentityUser
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
    }
}
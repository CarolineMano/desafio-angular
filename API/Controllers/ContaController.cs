using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTO;
using API.Helpers;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly SignInManager<Usuario> _signInManager;
        private readonly UserManager<Usuario> _userManager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _database;
        private readonly CpfHelper _cpfHelper;

        public ContaController(SignInManager<Usuario> signInManager, UserManager<Usuario> userManager, IConfiguration config, ApplicationDbContext database)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _database = database;
            _cpfHelper = new CpfHelper();
        }                

        [HttpPost("registrar")]
        public async Task<IActionResult> Register([FromBody] RegistroDto model)
        {
            if(ModelState.IsValid)
            {
                if(await _cpfHelper.ValidarCpf(model.Cpf) == false)
                {
                    return BadRequest("CPF inválido");
                }
                var userFromDb = await _userManager.FindByEmailAsync(model.Email);
                if(userFromDb == null)
                {
                    var role = _database.Roles.First(role => role.NormalizedName.Equals("COMUM"));
                    Usuario novoUsuario = new Usuario();
                    novoUsuario.UserName = model.Email;
                    novoUsuario.Email = model.Email;
                    novoUsuario.EmailConfirmed = true;
                    novoUsuario.Cpf = model.Cpf;
                    novoUsuario.Nome = model.Nome;

                    IdentityResult result = _userManager.CreateAsync(novoUsuario, model.Senha).Result;
                    await _userManager.AddToRoleAsync(novoUsuario, role.Name);

                    return Created("", new{userId = novoUsuario.Id, userName = model.Email});

                }
                return BadRequest("Email já cadastrado");
            }
            return BadRequest();
        }        

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if(ModelState.IsValid)
            {
                var userFromDb = await _userManager.FindByEmailAsync(model.Email);
                if(userFromDb != null)
                {
                    var passwordCheck = await _signInManager.CheckPasswordSignInAsync(userFromDb, model.Senha, false);
                    if(passwordCheck.Succeeded)
                    {
                        var token = Generate(userFromDb);
                        return Ok(token);
                    }
                    else
                    {
                        return Unauthorized();
                    }
                }
            }
            return BadRequest();
        }
        private string Generate(Usuario usuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            string roleId = _database.UserRoles.FirstOrDefault(role => role.UserId == usuario.Id).RoleId;

            var claims = new List<Claim>();
            claims.Add(new Claim("id", usuario.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, usuario.Email));
            claims.Add(new Claim("nome", usuario.Nome));
            claims.Add(new Claim("cpf", usuario.Cpf));
            claims.Add(new Claim(ClaimTypes.Role, _database.Roles.First(role => role.Id == roleId).ToString()));

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            return (new JwtSecurityTokenHandler().WriteToken(token));
        }        
    }
}
using System;
using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CarrinhoDeCompra> Carrinhos { get; set; }
        public DbSet<ItemCompra> Itens { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedRoles(builder);
            this.SeedProdutos(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<Usuario>();

            IdentityRole role1 = new IdentityRole()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };  

            IdentityRole role2 = new IdentityRole()
            {
                Name = "Comum",
                NormalizedName = "COMUM"
            };

            builder.Entity<IdentityRole>().HasData(role1);
            builder.Entity<IdentityRole>().HasData(role2);

            Usuario admin = new Usuario()
            {
                Id = "58f4c99a-fabd-4946-bc52-34416a856353",
                Nome = "Admin",
                Cpf = "00000000000",
                UserName = "admin@email.com",
                Email = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "Gft2022"),
                EmailConfirmed = true,
            };


            Usuario user = new Usuario()
            {
                Id = "f707c067-6020-4f65-87c2-c66e0120180d",
                Nome = "Joel",
                Cpf = "12345678911",
                UserName = "joel@email.com",
                Email = "joel@email.com",
                NormalizedUserName = "JOEL@EMAIL.COM",
                NormalizedEmail = "JOEL@EMAIL.COM",
                PasswordHash = hasher.HashPassword(null, "Gft2022"),
                EmailConfirmed = true,
            };

            builder.Entity<Usuario>().HasData(admin);
            builder.Entity<Usuario>().HasData(user);

            IdentityUserRole<string> userRole1 = new IdentityUserRole<string>()
            {
                RoleId = role1.Id,
                UserId = admin.Id
            };            

            IdentityUserRole<string> userRole2 = new IdentityUserRole<string>()
            {
                RoleId = role2.Id,
                UserId = user.Id
            }; 

            builder.Entity<IdentityUserRole<string>>().HasData(userRole1);      
            builder.Entity<IdentityUserRole<string>>().HasData(userRole2);            
        }

        private void SeedProdutos(ModelBuilder builder)
        {
            Produto produto1 = new Produto()
            {
                Id = 1,
                Marca = "Lindt",
                Descricao = "Trufas de chocolate ao leite com recheio cremoso",
                UnidadeDeMedida = "pacote 200g",
                Foto = "Lindt-Lindor.jpg",
                PrecoUnitario = 78.00f,
                QuantidadeEstoque = 15,
                Ativo = true
            };

            Produto produto2 = new Produto()
            {
                Id = 2,
                Marca = "Godiva",
                Descricao = "Chocolate 72% cacau com amêndoa",
                UnidadeDeMedida = "barra 90g",
                Foto = "Godiva-Almond.jpg",
                PrecoUnitario = 39.99f,
                QuantidadeEstoque = 10,
                Ativo = true
            };            

            builder.Entity<Produto>().HasData(produto1);
            builder.Entity<Produto>().HasData(produto2);
        }
    }
}
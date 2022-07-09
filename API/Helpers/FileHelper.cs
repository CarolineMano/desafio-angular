using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace API.Helpers
{
    public class FileHelper
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileHelper(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        public string UploadedFile(IFormFile imagem, string marca)
        {
            string nomeImagem = "GFT_Logo.jpg";

            if(imagem != null)
            {
                string pastaUpload = Path.Combine(_hostEnvironment.WebRootPath, "Images");
                string extensao = Path.GetExtension(imagem.FileName);
                nomeImagem = marca + "_" + DateTime.Now.ToString("yyMMddHHmmssfff") + extensao;
                string caminhoArquivo = Path.Combine(pastaUpload, nomeImagem);

                using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    imagem.CopyTo(fileStream);
                }            
            }
            return nomeImagem;
        }
    }
}
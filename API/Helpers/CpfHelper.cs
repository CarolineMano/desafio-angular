using System;
using System.Net.Http;
using System.Threading.Tasks;
using API.DTO;
using Newtonsoft.Json;

namespace API.Helpers
{
    public class CpfHelper
    {
        private readonly string _enderecoConsultaCpf;
        private readonly string _tipoConsultaCpf;

        public CpfHelper()
        {
            _enderecoConsultaCpf = "https://api.invertexto.com/v1/validator?token=333%7Ctb08rjo5vlly8JLk3qJbMkE1y4PKuoIZ&value=";
            _tipoConsultaCpf = "&type=cpf";
        }

        public async Task<bool> ValidarCpf(string cpf)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_enderecoConsultaCpf + cpf + _tipoConsultaCpf);
            var response = await client.GetAsync("");

            string content = await response.Content.ReadAsStringAsync();

            var retornoApi = JsonConvert.DeserializeObject<CpfDto>(content);

            return retornoApi.Valid;
        }

    }
}
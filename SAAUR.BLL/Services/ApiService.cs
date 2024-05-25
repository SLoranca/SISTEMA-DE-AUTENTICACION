using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.ServicesApi
{
    public class ApiService : IApiService
    {
        private readonly IConfiguration _config;
        public ApiService(IConfiguration config)
		{
			_config = config;	
        }

        public async Task<ModelResponse> apiPost(object parameters, string client, string uri)
        {
            HttpClient httpClient = new();
            ModelResponse result = new();
            if (httpClient.BaseAddress == null){ httpClient.BaseAddress = new Uri(_config["ApiSettings:urlApi" + client]); }
            var content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(_config["ApiSettings:api" + uri], content);
			var apiResult = await response.Content.ReadAsStringAsync();
			return result = JsonConvert.DeserializeObject<ModelResponse>(apiResult);
        }
    }
}
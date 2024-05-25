using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IApiService
    {
        Task<ModelResponse> apiPost(object parameters, string client, string uri);
    }
}
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IAccountService
    {
        ModelResponse Authentication(string email, string password);
        ModelResponse Create(ModelAccountCreate model);
        ModelResponse Recovery(string email, string password, string hashPass, string salt);
    }
}
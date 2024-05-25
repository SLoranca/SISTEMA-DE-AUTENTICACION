using SAAUR.MODELS.Entities;

namespace SAAUR.DATA.Interfaces
{
    public interface IAccountRepository
    {
        ModelResponse Authentication(string email, string password);
        public ModelResponse Create(ModelAccountCreate model);
        public ModelResponse Recovery(string email, string password, string hashPass, string salt);
    }
}
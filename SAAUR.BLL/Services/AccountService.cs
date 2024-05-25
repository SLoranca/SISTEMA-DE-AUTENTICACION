using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _authRepository;

        public AccountService(IAccountRepository authRepository)
		{
			_authRepository = authRepository;
		}

        public ModelResponse Authentication(string email, string password)
		{
			return _authRepository.Authentication(email, password);
		}

        public ModelResponse Create(ModelAccountCreate model)
        {
            return _authRepository.Create(model);
        }

        public ModelResponse Recovery(string email, string password, string hashPass, string salt)
        {
            return _authRepository.Recovery(email, password, hashPass, salt);
        }
    }
}
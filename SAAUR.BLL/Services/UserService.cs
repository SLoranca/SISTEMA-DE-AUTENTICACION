using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ModelResponse Get()
        {
            return _userRepository.Get();
        }

		public ModelResponse Insert(ModelUser model)
		{
			return _userRepository.Insert(model);
		}

        public ModelResponse UpdGeneralInfo(ModelUserEditGeneralInfo model)
		{
			return _userRepository.UpdGeneralInfo(model);
		}

        public ModelResponse UpdEmail(ModelUserEditEmail model)
		{
			return _userRepository.UpdEmail(model);
		}

        public ModelResponse UpdPassword(ModelUserEditPassword model)
		{
			return _userRepository.UpdPassword(model);
		}

		public ModelResponse Delete(int id)
		{
			return _userRepository.Delete(id);
		}
    }
}
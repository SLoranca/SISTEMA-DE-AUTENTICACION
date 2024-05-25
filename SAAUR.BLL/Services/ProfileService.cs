using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

		public ProfileService(IProfileRepository profileRepository)
		{
			_profileRepository = profileRepository;
		}

		public ModelResponse UpdProfile(ModelProfile model)
		{
			return _profileRepository.UpdProfile(model);
		}

		public ModelResponse UpdPassword(ModelProfilePassword model)
		{
			return _profileRepository.UpdPassword(model);
		}

        public ModelResponse UpdEmail(ModelProfileEmail model)
        {
            return _profileRepository.UpdEmail(model);
        }

		public ModelResponse EnableTwoFactor(int user_id)
        {
            return _profileRepository.EnableTwoFactor(user_id);
        }
		
		public ModelResponse DisableTwoFactor(int user_id)
        {
            return _profileRepository.DisableTwoFactor(user_id);
        }
    }
}
using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
    public class AppService : IAppService
    {
        private readonly IAppRepository _appRepository;

		public AppService(IAppRepository appRepository)
		{
			_appRepository = appRepository;
		}

		public ModelResponse Get()
		{
			return _appRepository.Get();
		}

		public ModelResponse Insert(ModelApplication model)
		{
			return _appRepository.Insert(model);
		}

		public ModelResponse Update(ModelApplication model)
		{
			return _appRepository.Update(model);
		}

		public ModelResponse Delete(int id)
		{
			return _appRepository.Delete(id);
		}
    }
}
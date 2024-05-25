using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
	public class ModuleService : IModuleService
	{
		private readonly IModuleRepository _moduleRepository;

		public ModuleService(IModuleRepository moduleRepository)
		{
			_moduleRepository = moduleRepository;
		}

		public ModelResponse Get(int app_id)
		{
			return _moduleRepository.Get(app_id);
		}

		public ModelResponse Insert(ModelModule model)
		{
			return _moduleRepository.Insert(model);
		}

		public ModelResponse Update(ModelModule model)
		{
			return _moduleRepository.Update(model);
		}

		public ModelResponse Enable(int id)
		{
			return _moduleRepository.Enable(id);
		}

        public ModelResponse Disable(int id)
        {
            return _moduleRepository.Disable(id);
        }
    }
}

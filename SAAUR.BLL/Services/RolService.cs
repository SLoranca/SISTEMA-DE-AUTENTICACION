using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
	public class RolService : IRolService
	{
		private readonly IRolRepository _rolRepository;

		public RolService(IRolRepository rolRepository)
		{
			_rolRepository = rolRepository;
		}

		public ModelResponse Get()
		{
			return _rolRepository.Get();
		}

		public ModelResponse Insert(ModelRol model)
		{
			return _rolRepository.Insert(model);
		}

		public ModelResponse Update(ModelRol model)
		{
			return _rolRepository.Update(model);
		}

		public ModelResponse Delete(int id)
		{
			return _rolRepository.Delete(id);
		} 

        #region CONFIGURACIÓN DE PERMISOS A ROLES
        public ModelResponse ListRolApp()
        {
            return _rolRepository.ListRolApp();
        }

        public ModelResponse ListRolModules(int app_id)
        {
            return _rolRepository.ListRolModules(app_id);
        }

        public ModelResponse InsertRolAppModules(ModelRolApp model)
        {
            return _rolRepository.InsertRolAppModules(model);
        }
        #endregion
    }
}
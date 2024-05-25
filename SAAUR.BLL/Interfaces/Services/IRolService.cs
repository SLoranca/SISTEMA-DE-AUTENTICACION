using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IRolService
    {
        ModelResponse Get();
        ModelResponse Insert(ModelRol model);
        ModelResponse Update(ModelRol model);
        ModelResponse Delete(int id);

        #region CONFIGURACIÓN DE PERMISOS A ROLES
        ModelResponse ListRolApp();
        ModelResponse ListRolModules(int app_id);
        ModelResponse InsertRolAppModules(ModelRolApp model);
        #endregion
    }
}
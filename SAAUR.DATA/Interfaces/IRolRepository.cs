using SAAUR.MODELS.Entities;

namespace SAAUR.DATA.Interfaces
{
    public interface IRolRepository
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
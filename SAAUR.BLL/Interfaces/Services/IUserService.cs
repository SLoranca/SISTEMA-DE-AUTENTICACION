using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IUserService
    {
        ModelResponse Get();
        ModelResponse Insert(ModelUser model);
        ModelResponse UpdGeneralInfo(ModelUserEditGeneralInfo model);
        ModelResponse UpdEmail(ModelUserEditEmail model);
        ModelResponse UpdPassword(ModelUserEditPassword model);
        ModelResponse Delete(int id);
    }
}
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IActivityService
    {
        ModelResponse Get_by_id(int item_id);
        ModelResponse Get_by_id_user(int user_id);
        ModelResponse Insert(ModelActivity model);
        ModelResponse Complete(int act_id);
        ModelResponse Delete(int act_id);
    }
}
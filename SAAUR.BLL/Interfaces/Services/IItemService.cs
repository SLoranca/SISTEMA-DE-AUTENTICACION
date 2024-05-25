using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IItemService
    {
        ModelResponse Get(int user_id);
        ModelResponse Insert(ModelItem model);
    }
}
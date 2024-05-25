using SAAUR.MODELS.Entities;

namespace SAAUR.DATA.Interfaces
{
    public interface IItemRepository
    {
        ModelResponse Get(int user_id);
        ModelResponse Insert(ModelItem model);
    }
}
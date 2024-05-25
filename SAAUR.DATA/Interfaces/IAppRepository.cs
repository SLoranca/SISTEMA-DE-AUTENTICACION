using SAAUR.MODELS.Entities;

namespace SAAUR.DATA.Interfaces
{
    public interface IAppRepository
    {
        ModelResponse Get();
        ModelResponse Insert(ModelApplication model);
        ModelResponse Update(ModelApplication model);
        ModelResponse Delete(int id);
    }
}
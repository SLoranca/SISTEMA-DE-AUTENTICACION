using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IAppService
    {
        ModelResponse Get();
        ModelResponse Insert(ModelApplication model);
        ModelResponse Update(ModelApplication model);
        ModelResponse Delete(int id);
    }
}
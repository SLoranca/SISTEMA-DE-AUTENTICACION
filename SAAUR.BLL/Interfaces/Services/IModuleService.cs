using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
	public interface IModuleService
	{
		ModelResponse Get(int app_id);
		ModelResponse Insert(ModelModule model);
		ModelResponse Update(ModelModule model);
		ModelResponse Enable(int id);
        ModelResponse Disable(int id);
	}
}

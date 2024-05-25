using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IProfileService
    {
        ModelResponse UpdProfile(ModelProfile model);
        ModelResponse UpdPassword(ModelProfilePassword model);
        ModelResponse UpdEmail(ModelProfileEmail model);
        ModelResponse EnableTwoFactor(int user_id);
        ModelResponse DisableTwoFactor(int user_id);
    }
}
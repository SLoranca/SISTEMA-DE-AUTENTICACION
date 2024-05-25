using SAAUR.MODELS.Entities;

namespace SAAUR.DATA.Interfaces
{
    public interface IProfileRepository
    {
        ModelResponse UpdProfile(ModelProfile model);
        ModelResponse UpdPassword(ModelProfilePassword model);
        ModelResponse UpdEmail(ModelProfileEmail model);
        ModelResponse EnableTwoFactor(int user_id);
        ModelResponse DisableTwoFactor(int user_id);
    }
}
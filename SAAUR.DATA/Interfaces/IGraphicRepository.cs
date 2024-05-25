using SAAUR.MODELS.Entities;

namespace SAAUR.DATA.Interfaces
{
    public interface IGraphicRepository
    {
        ModelResponse GraphicsPerfomanceGeneral(string status);
    }
}
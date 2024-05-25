using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
    public class GraphicService : IGraphicService
    {
        private readonly IGraphicRepository _graphicsRepository;

        public GraphicService(IGraphicRepository graphicsRepository)
        {
            _graphicsRepository = graphicsRepository;
        }

        public ModelResponse GraphicsPerfomanceGeneral(string status)
        {
            return _graphicsRepository.GraphicsPerfomanceGeneral(status);
        }
    }
}
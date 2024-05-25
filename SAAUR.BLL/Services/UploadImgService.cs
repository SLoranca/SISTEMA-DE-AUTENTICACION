using SAAUR.BLL.Interfaces.Services;
using SAAUR.DATA.Interfaces;
using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Services
{
    public class UploadImgService : IUploadImgService
    {
        private readonly IUploadImgRepository _uploadImgRepository;

        public UploadImgService(IUploadImgRepository uploadImgRepository)
        {
            _uploadImgRepository = uploadImgRepository;
        }

        public ModelResponse UploadImg(ModelImgUpload model)
        {
            return _uploadImgRepository.UploadImg(model);
        }
    }
}
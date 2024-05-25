using SAAUR.MODELS.Entities;

namespace SAAUR.BLL.Interfaces.Services
{
    public interface IUploadImgService
    {
        ModelResponse UploadImg(ModelImgUpload model);
    }
}
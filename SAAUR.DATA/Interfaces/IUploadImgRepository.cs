using SAAUR.MODELS.Entities;

namespace SAAUR.DATA.Interfaces
{
    public interface IUploadImgRepository
    {
        ModelResponse UploadImg(ModelImgUpload model);
    }
}
using Microsoft.AspNetCore.Mvc;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.MODELS.Entities;

namespace SAAUR.APP_WEB.Controllers
{
    public class UploadImgController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUploadImgService _uploadImgService;

        public UploadImgController(IWebHostEnvironment webHostEnvironment, IUploadImgService uploadImgService)
        {
            _webHostEnvironment = webHostEnvironment;
            _uploadImgService = uploadImgService;
        }

        public IActionResult UploadImg(IFormFile file, int user_id)
        {
            ModelResponse result = new();
            try
            {
                if (file.ContentType != "image/jpeg")
                {
                    result.status = "ERROR";
                    result.message = "Tipo de archivo no soportado, solo se aceptan imagenes (jpeg)";
                    return Json(result);
                }

                ModelImgUpload model = new();
                model.user_id = user_id;
                model.image = file.FileName;

                if (ModelState.IsValid)
                {
                    result = _uploadImgService.UploadImg(model);
                    if (result.status == "OK")
                    {
                        string pathBase = _webHostEnvironment.ContentRootPath;
                        string[] pathsFolder = { pathBase, "wwwroot", "profile/img", model.user_id.ToString() };
                        string folder = Path.Combine(pathsFolder);

                        bool folderExists = Directory.Exists(folder);
                        if (folderExists)
                        {
                            Directory.Delete(folder, true);
                            Directory.CreateDirectory(folder);
                        }
                        else
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string filePath = Path.Combine(folder, file.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyToAsync(stream);
                        }

                        result.status = "OK";
                        result.message = result.message;
                    }
                }
                else
                {
                    result.status = "ERROR";
                    result.data = null;
                    result.message = "Modelo invalido";
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.data = null;
                result.message = ex.Message.ToString();
            }

            return Json(result);
        }
    }
}
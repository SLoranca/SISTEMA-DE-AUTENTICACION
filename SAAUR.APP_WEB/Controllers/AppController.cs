using Microsoft.AspNetCore.Mvc;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.MODELS.Entities;

namespace SAAUR.APP_WEB.Controllers
{
    public class AppController : Controller
    {
        private readonly IAppService _appService;

        public AppController(IAppService appService)
        {
            _appService = appService;
        }

        public IActionResult GetList()
        {
            ModelResponse result = _appService.Get();
            return Json(result);
        }

        public IActionResult Insert(ModelApplication model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _appService.Insert(model);
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

        public IActionResult Update(ModelApplication model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _appService.Update(model);
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

        public IActionResult Delete(int id)
        {
            ModelResponse result = _appService.Delete(id);
            return Json(result);
        }
    }
}
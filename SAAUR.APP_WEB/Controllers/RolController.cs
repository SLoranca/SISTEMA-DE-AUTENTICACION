using Microsoft.AspNetCore.Mvc;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.MODELS.Entities;

namespace SAAUR.APP_WEB.Controllers
{
    public class RolController : Controller
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        public IActionResult GetList()
        {
            ModelResponse result = _rolService.Get();
            return Json(result);
        }

        public IActionResult Insert(ModelRol model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _rolService.Insert(model);
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

        public IActionResult Update(ModelRol model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _rolService.Update(model);
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
            ModelResponse result = _rolService.Delete(id);
            return Json(result);
        }

        #region CONFIGURACIÓN DE PERMISOS A ROLES
        public IActionResult ListRolApp()
        {
            ModelResponse result = _rolService.ListRolApp();
            return Json(result);
        }

        public IActionResult ListRolModules(int app_id)
        {
            ModelResponse result = _rolService.ListRolModules(app_id);
            return Json(result);
        }

        public IActionResult InsertRolAppModules(ModelRolApp model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _rolService.InsertRolAppModules(model);
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
        #endregion

    }
}
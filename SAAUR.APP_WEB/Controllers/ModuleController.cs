using Microsoft.AspNetCore.Mvc;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.MODELS.Entities;

namespace SAAUR.APP_WEB.Controllers
{
	public class ModuleController : Controller
	{
		private readonly IModuleService _moduleService;

		public ModuleController(IModuleService moduleService)
		{
			_moduleService = moduleService;
		}

		public IActionResult GetList(int app_id)
		{
			ModelResponse result = _moduleService.Get(app_id);
			return Json(result);
		}

		public IActionResult Insert(ModelModule model)
		{
			ModelResponse result = new();
			try
			{
				if (ModelState.IsValid)
				{
					result = _moduleService.Insert(model);
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

		public IActionResult Update(ModelModule model)
		{
			ModelResponse result = new();
			try
			{
				if (ModelState.IsValid)
				{
					result = _moduleService.Update(model);
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

        public IActionResult Enable(int id)
        {
            ModelResponse result = _moduleService.Enable(id);
            return Json(result);
        }

        public IActionResult Disable(int id)
		{
			ModelResponse result = _moduleService.Disable(id);
			return Json(result);
		}
	}
}

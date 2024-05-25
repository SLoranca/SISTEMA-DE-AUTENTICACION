using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.MODELS.Entities;

namespace SAAUR.APP_WEB.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public IActionResult GetbyId(int item_id)
        {
            ModelResponse result = _activityService.Get_by_id(item_id);
            return Json(result);
        }

        public IActionResult GetbyIdUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            ModelResponse result = _activityService.Get_by_id_user(user_id);
            return Json(result);
        }

        public IActionResult Insert(ModelActivity model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _activityService.Insert(model);
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

        public IActionResult Complete(int act_id)
        {
            ModelResponse result = _activityService.Complete(act_id);
            return Json(result);
        }

        public IActionResult Delete(int act_id)
        {
            ModelResponse result = _activityService.Delete(act_id);
            return Json(result);
        }
    }
}
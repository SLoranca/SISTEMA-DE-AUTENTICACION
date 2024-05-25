using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.MODELS.Entities;

namespace SAAUR.APP_WEB.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        public IActionResult GetList()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            ModelResponse result = _itemService.Get(user_id);
            return Json(result);
        }

        public IActionResult Insert(ModelItem model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var identity = (ClaimsIdentity)User.Identity;
                    var user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    model.user_id = user_id;
                    result = _itemService.Insert(model);
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
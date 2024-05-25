using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.BLL.Interfaces.Tools;
using SAAUR.MODELS.Entities;

namespace SAAUR.APP_WEB.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly IPasswordTool _passwordTool;
        private readonly IGraphicService _graphicService;

        public ProfileController(IProfileService profileService, IPasswordTool passwordTool, IGraphicService graphicService)
        {
            _profileService = profileService;
            _passwordTool = passwordTool;
            _graphicService = graphicService;
        }

        public IActionResult GetGraphByStatus(string status)
        {
            ModelResponse result = _graphicService.GraphicsPerfomanceGeneral(status);
            return Json(result);
        }

        public IActionResult UpdProfile(ModelProfile model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var identity = (ClaimsIdentity)User.Identity;
                    var user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    model.user_id = user_id;
                    result = _profileService.UpdProfile(model);
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

        public IActionResult UpdPassword(ModelProfilePassword model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.new_password != model.confirm_password)
                    {
                        result.status = "ERROR";
                        result.data = null;
                        result.message = "Las contraseñas no coninciden";
                        return Json(result);
                    }

                    var identity = (ClaimsIdentity)User.Identity;
                    var user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var cryptoPass = _passwordTool.CryptoEncrypt(model.confirm_password);
                    var hash = _passwordTool.GenerateSalt(cryptoPass);
                    model.user_id = user_id;
                    model.password = cryptoPass;
                    model.hashPass = hash.hashPass;
                    model.salt = hash.salt;
                    result = _profileService.UpdPassword(model);
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

        public IActionResult UpdEmail(ModelProfileEmail model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var identity = (ClaimsIdentity)User.Identity;
                    var user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var cryptoPass = _passwordTool.CryptoEncrypt(model.confirm_password);
                    model.user_id = user_id;
                    model.confirm_password = cryptoPass;
                    result = _profileService.UpdEmail(model);
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

        public IActionResult EnableTwoFactor()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            ModelResponse result = _profileService.EnableTwoFactor(user_id);
            return Json(result);
        }

        public IActionResult DIsableTwoFactor()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            ModelResponse result = _profileService.DisableTwoFactor(user_id);
            return Json(result);
        }
    }
}
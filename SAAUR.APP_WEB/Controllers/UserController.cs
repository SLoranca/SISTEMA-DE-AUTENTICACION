using Microsoft.AspNetCore.Mvc;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.BLL.Interfaces.Tools;
using SAAUR.MODELS.Entities;

namespace SAAUR.APP_WEB.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPasswordTool _passwordTool;

        public UserController(IUserService userService, IPasswordTool passwordTool)
        {
            _userService = userService;
            _passwordTool = passwordTool;
        }

        public IActionResult GetList()
        {
            ModelResponse result = _userService.Get();
            return Json(result);
        }

        public IActionResult Insert(ModelUser model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var cryptoPass = _passwordTool.CryptoEncrypt(model.password);
                    var hash = _passwordTool.GenerateSalt(cryptoPass);
                    model.password = cryptoPass;
                    model.hashPass = hash.hashPass;
                    model.salt = hash.salt;
                    result = _userService.Insert(model);
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

        public IActionResult UpdGeneralInfo(ModelUserEditGeneralInfo model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _userService.UpdGeneralInfo(model);
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

        public IActionResult UpdEmail(ModelUserEditEmail model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    result = _userService.UpdEmail(model);
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

        public IActionResult UpdPassword(ModelUserEditPassword model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var cryptoPass = _passwordTool.CryptoEncrypt(model.password);
                    var hash = _passwordTool.GenerateSalt(cryptoPass);
                    model.password = cryptoPass;
                    model.hashPass = hash.hashPass;
                    model.salt = hash.salt;
                    result = _userService.UpdPassword(model);
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
            ModelResponse result = _userService.Delete(id);
            return Json(result);
        }
    }
}
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SAAUR.BLL.Interfaces.Services;
using SAAUR.BLL.Interfaces.Tools;
using SAAUR.MODELS.Entities;
using SAAUR.MODELS.Entities.TemplatesEmail;

namespace SAAUR.APP_WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IApiService _apiService;
        private readonly IAccountService _authService;
        private readonly IPasswordTool _passwordTool;

        public AccountController(IAccountService authService, IPasswordTool passwordTool, IConfiguration config, IApiService apiService)
        {
            _authService = authService;
            _passwordTool = passwordTool;
            _config = config;
            _apiService = apiService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login2FA()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Recovery()
        {
            return View();
        }

        public IActionResult Profile()
        {
            if (User.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)User.Identity;
                var user_id = 0;
                var name = "";
                var rol = "";
                var photo = "";
                var src_img = "../profile/";

                if (identity != null)
                {
                    var _photo_def = identity.FindFirst("Photo").Value;
                    user_id = Convert.ToInt32(identity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    name = identity.FindFirst(ClaimTypes.Name).Value;
                    rol = identity.FindFirst(ClaimTypes.Role).Value;
                    if (_photo_def == "blank.png")
                        photo = src_img + "blank.png";
                    else
                        photo = src_img + "img" + "/" + user_id + "/" + identity.FindFirst("Photo").Value;
                }
                else
                {
                    user_id = 0;
                    name = "S/I";
                    rol = "S/I";
                    photo = src_img + "blank.png";
                }

                ViewBag.User_id = user_id;
                ViewBag.Name = name;
                ViewBag.Rol = rol;
                ViewBag.Photo = photo;

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Authentication(ModelAuth model)
        {
            ModelInfoResult resultInfo = new();
            ModelResponse result = new();

            try
            {
                if (ModelState.IsValid)
                {
                    result = _authService.Authentication(model.email, _passwordTool.CryptoEncrypt(model.pwd));

                    if (result.status == "OK")
                    {
                        ModelTwoFactorInteger userInfo = JsonConvert.DeserializeObject<ModelTwoFactorInteger>(result.data);
                        var IsVerified = _passwordTool.VerifyHashPassword(_passwordTool.CryptoEncrypt(model.pwd), userInfo.hashPass, userInfo.salt);

                        if (!IsVerified)
                        {
                            resultInfo.status = "ERROR";
                            resultInfo.message = "Ocurrio un error al intentar verificar sus credenciales.";
                            return Json(resultInfo);
                        }

                        result = await _apiService.apiPost(userInfo, "Authentication", "Token");

                        if (result.status != "OK")
                        {
                            resultInfo.status = "ERROR";
                            resultInfo.message = result.message;
                            return Json(resultInfo);
                        }

                        resultInfo.token = result.data;
                        resultInfo.json_apps = userInfo.json_apps;
                        resultInfo.json_modules = userInfo.json_modules;
                        resultInfo.twofactor = userInfo.twofactor;
                        resultInfo.email = userInfo.email;

                        if (userInfo.twofactor == 1)
                        {
                            ModelAuth2FA modelAuth2FA = new ModelAuth2FA() { email = model.email };
                            result = await _apiService.apiPost(modelAuth2FA, "Authentication", "Auth2FA");

                            if (result.status == "OK")
                            {
                                var parameters = new Auth2FactorTemplateEmail(model.email, result.data);
                                result = await _apiService.apiPost(parameters, "Tools", "SendEmail");
                                
                                if (result.status != "OK") {
                                    resultInfo.status = "ERROR";
                                    resultInfo.message = result.message;
                                    return Json(resultInfo);
                                }

                                resultInfo.Url = Url.Action("Login2FA", "Account");
                            }
                            else
                            {
                                resultInfo.status = "ERROR";
                                resultInfo.message = result.message;
                                return Json(resultInfo);
                            }
                        }
                        else
                        {
                            CreateCookie(userInfo);
                            resultInfo.Url = Url.Action("Profile", "Account");
                        }
                    }
                    else
                    {
                        resultInfo.message = result.message;
                    }
                }
                else
                {
                    resultInfo.status = "ERROR";
                    resultInfo.message = "Modelo Invalido";
                }

                resultInfo.status = result.status;
            }
            catch (Exception ex)
            {
                resultInfo.status = "ERROR";
                resultInfo.data = null;
                resultInfo.message = ex.Message.ToString();
            }

            return Json(resultInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Authentication2FAValid(ModelAuth2FAValid model)
        {
            ModelInfoResult resultInfo = new();
            ModelResponse result = new();

            try
            {
                if (ModelState.IsValid)
                {
                    result = await _apiService.apiPost(model, "Authentication", "Auth2FAValid");
                    if (result.status == "OK")
                    {
                        int twofactor = 0;
                        result.data = JsonConvert.SerializeObject(result.data);
                        ModelTwoFactorString userInfo = JsonConvert.DeserializeObject<ModelTwoFactorString>(result.data);

                        result = await _apiService.apiPost(userInfo, "Tools", "Token");

                        if (result.status != "OK")
                        {
                            resultInfo.status = "ERROR";
                            resultInfo.message = result.message;
                            return Json(resultInfo);
                        }

                        if (userInfo.twofactor != null) { twofactor = 1; }

                        resultInfo.token = result.data;
                        resultInfo.json_apps = userInfo.json_apps;
                        resultInfo.json_modules = userInfo.json_modules;
                        resultInfo.twofactor = twofactor;

                        CreateCookie(userInfo);
                        resultInfo.Url = Url.Action("Profile", "Account");
                    }
                    else
                    {
                        resultInfo.message = result.message;
                    }
                }
                else
                {
                    resultInfo.status = "ERROR";
                    resultInfo.message = "Modelo Invalido";
                }

                resultInfo.status = result.status;
            }
            catch (Exception ex)
            {
                resultInfo.status = "ERROR";
                resultInfo.data = null;
                resultInfo.message = ex.Message.ToString();
            }

            return Json(resultInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Recovery(ModelAccountRecovery model)
        {
            ModelResponse result = new();
            try
            {
                if (ModelState.IsValid)
                {
                    var newPassword = _passwordTool.Generate(4, 4);
                    var cryptoPass = _passwordTool.CryptoEncrypt(newPassword);
                    var hash = _passwordTool.GenerateSalt(cryptoPass);
                    result = _authService.Recovery(model.destination_email, cryptoPass, hash.hashPass, hash.salt);
                    if (result.status == "OK")
                    {
                        var parameters = new AccountRecoveryTemplateEmail(model.destination_email, newPassword);
                        result = await _apiService.apiPost(parameters, "Tools", "SendEmail");
                        if (result.status == "OK")
                        {
                            result.status = "OK";
                            result.message = result.message;
                        }
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

        [HttpPost]
        private async Task<IActionResult> GenerateCode(string email)
        {
            ModelResponse result = new();

            try
            {
                if (String.IsNullOrEmpty(email))
                {
                    result.status = "ERROR";
                    result.data = null;
                    result.message = "El email no puede estar vacio, intente de nuevo";
                    return Json(result);
                }

                ModelAuth2FA modelAuth2FA = new ModelAuth2FA() { email = email };
                result = await _apiService.apiPost(modelAuth2FA, "Authentication", "Auth2FA");
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.data = null;
                result.message = ex.Message.ToString();
            }

            return Json(result);
        }

        [HttpPost]
        public IActionResult Create(ModelAccountCreate model)
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
                    result = _authService.Create(model);
                    if (result.status == "OK")
                    {
                        result.status = "OK";
                        result.data = Url.Action("Login", "Account");
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
        
        private void CreateCookie(ModelUserDeserialize userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userInfo.id)),
                new Claim(ClaimTypes.Name, userInfo.name + " " + userInfo.p_last_name + " " + userInfo.m_last_name),
                new Claim(ClaimTypes.Role, userInfo.rol),
                new Claim("Photo", userInfo.img)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

    }
}
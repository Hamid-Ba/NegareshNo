using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.Consultant;

namespace NegareshNo.Controllers
{
    public class IdentityController : Controller
    {

        private readonly IConsultantService consultantService;

        public IdentityController(IConsultantService consultantService) => this.consultantService = consultantService;

        [Route("Login")]
        [HttpGet]
        public IActionResult Login() => View();

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(ConsultantLoginVM ViewModel)
        {
            if (!ModelState.IsValid) return View(ViewModel);

            if (!await consultantService.CanUserLogin(ViewModel.UserName, ViewModel.Password))
            {
                ModelState.AddModelError("UserName", "نام کاربری یا رمزعبور شما صحیح نمی باشد!");
                return View(ViewModel);
            }

            var user = await consultantService.GetConsultantByUserName(ViewModel.UserName);

            if (user != null)
            {

                var claims = new List<Claim>()
                {
                   new Claim(ClaimTypes.NameIdentifier,user.ConsultantId.ToString()),
                   new Claim(ClaimTypes.Name,user.UserName)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principle = new ClaimsPrincipal(identity);

                var properties = new AuthenticationProperties()
                {
                    IsPersistent = ViewModel.RememberMe
                };

                await HttpContext.SignInAsync(principle, properties);
                ViewBag.IsSuccess = true;
                return View();

            }
            ModelState.AddModelError("Email", "کاربری با مشخصات وارد شده یافت نشد");
            return View(ViewModel);
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
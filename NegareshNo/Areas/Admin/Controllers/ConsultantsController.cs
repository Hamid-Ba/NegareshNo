using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NegareshNo.Core.Securities;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Context;
using NegareshNo.Data.Model.Consulting;

namespace NegareshNo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionChecker(2)]
    public class ConsultantsController : Controller
    {
        private readonly IConsultantService consultantService;
        private readonly IRoleService roleService;

        public ConsultantsController(IConsultantService consultantService, IRoleService roleService)
        {
            this.roleService = roleService;
            this.consultantService = consultantService;
        }

        [PermissionChecker(2)]
        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;
            return View(await consultantService.GetAllConsultantsIndexForAdminPanel());
        }

        [PermissionChecker(3)]
        public async Task<IActionResult> Details(int? consultantId)
        {
            if (consultantId == null || !await consultantService.IsExistConsultant((int)consultantId)) { return NotFound(); }

            ViewBag.ConsultantRoles = await consultantService.GetConsultantRolesTitle((int)consultantId);

            var consultant = await consultantService.GetConsultantById((int)consultantId);
            return PartialView(consultant);
        }

        [HttpGet]
        [PermissionChecker(3)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await roleService.GetAllRoles(), "RoleId", "RoleTitle");
            return View();
        }

        [HttpPost]
        [PermissionChecker(3)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consultant consultant, IFormFile ImageName,string ConfirmPassword, int Gender, int[] RolesId)
        {
            ViewBag.Roles = new SelectList(await roleService.GetAllRoles(), "RoleId", "RoleTitle");
            switch (Gender)
            {
                case 0:
                    consultant.Status = false;
                    break;
                case 1:
                    consultant.Status = true;
                    break;
            }

            if (consultant.Password != ConfirmPassword)
            {
                ModelState.AddModelError("Password", "رمز های عبور مطابقت ندارند!");
                return View(consultant);
            }

            if(await consultantService.IsConsultantUserNameExist(consultant.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری به ثبت رسیده است!");
                return View(consultant);
            }

            if (ModelState.IsValid)
            {
                await consultantService.CreateConsultant(consultant, ImageName, RolesId);
                return RedirectToAction(nameof(Index), new { message = "عملیات با موفقیت انجام شد" });
            }
            
            return View(consultant);
        }

        [HttpGet]
        [PermissionChecker(4)]
        public async Task<IActionResult> Edit(int? consultantId)
        {
            if (consultantId == null) return NotFound();

            var consultant = await consultantService.GetConsultantForEdit((int)consultantId);

            if (consultant == null) return NotFound();

            ViewBag.Roles = new SelectList(await roleService.GetAllRoles(), "RoleId", "RoleTitle");
            return View(consultant);
        }

        [HttpPost]
        [PermissionChecker(4)]
        public async Task<IActionResult> Edit(ConsultantEditVM ViewModel, IFormFile ImageName, string ConfirmPassword, int Gender)
        {
            ViewBag.Roles = new SelectList(await roleService.GetAllRoles(), "RoleId", "RoleTitle");
            if (!ModelState.IsValid) return View(consultantService.GetConsultantForEdit(ViewModel.ConsultantId));

            var userName = await consultantService.GetConsultantUserName(ViewModel.ConsultantId);

            if (ViewModel.Password != ConfirmPassword)
            {
                ModelState.AddModelError("Password", "رمز های عبور مطابقت ندارند!");
                return View(ViewModel);
            }

            if(ViewModel.UserName != userName && await consultantService.IsConsultantUserNameExist(ViewModel.UserName))
            {
                ModelState.AddModelError("UserName", "این نام کاربری به ثبت رسیده است!");
                return View(ViewModel);
            }

            switch (Gender)
            {
                case 0:
                    ViewModel.Status = false;
                    break;
                case 1:
                    ViewModel.Status = true;
                    break;
            }

            await consultantService.UpdateConsultant(ViewModel, ImageName);
            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }

        [PermissionChecker(5)]
        public async Task<IActionResult> Delete(int? consultantId)
        {
            if (consultantId == null) return NotFound();

            if (!await consultantService.IsExistConsultant((int)consultantId)) return NotFound();

            await consultantService.DeleteUser((int)consultantId);

            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NegareshNo.Core.Securities;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.Role;
using NegareshNo.Data.Context;
using NegareshNo.Data.Model.Access;

namespace NegareshNo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionChecker(16)]
    public class RolesController : Controller
    {
        private readonly IRoleService roleService;

        public RolesController(IRoleService roleService) => this.roleService = roleService;


        [PermissionChecker(16)]
        // GET: Admin/Roles
        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;
            return View(await roleService.GetAllRoles());
        }

        [PermissionChecker(17)]
        // GET: Admin/Roles/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Permmisions = new SelectList(await roleService.GetAllPermmisions(), "PermmisionId", "PermmisionTitle");
            return View();
        }

        // POST: Admin/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(17)]
        public async Task<IActionResult> Create(Role role, int[] PermmsionId)
        {
            ViewBag.Permmisions = new SelectList(await roleService.GetAllPermmisions(), "PermmisionId", "PermmisionTitle");
            if (!ModelState.IsValid) return View();
            if (await roleService.IsThisRoleExist(role.RoleTitle))
            {
                ModelState.AddModelError("RoleTitle", "نقشی با این عنوان موجود است!");
                return View();
            }

            int roleId = await roleService.AddRole(role);
            roleService.AddPermmisionToRole(PermmsionId, roleId);

            return RedirectToAction("Index");

        }

        [PermissionChecker(18)]
        // GET: Admin/Roles/Edit/5
        public async Task<IActionResult> Edit(int? roleId)
        {
            if (roleId == null)
            {
                return NotFound();
            }

            var role = roleService.GetRoleForEdit((int)roleId);

            if (role == null)
            {
                return NotFound();
            }

            ViewBag.Permmisions = new SelectList(await roleService.GetAllPermmisions(), "PermmisionId", "PermmisionTitle");
            return View(role);
        }

        // POST: Admin/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [PermissionChecker(18)]
        public async Task<IActionResult> Edit(RoleEditVM role)
        {
            var Role = await roleService.GetRoleById(role.RoleId);
            ViewBag.Permmisions = new SelectList(await roleService.GetAllPermmisions(), "PermmisionId", "PermmisionTitle");
            if (!ModelState.IsValid) return View(role);
            if (Role.RoleTitle != role.RoleTitle && await roleService.IsThisRoleExist(role.RoleTitle))
            {
                ModelState.AddModelError("RoleTitle", "نقشی با این عنوان موجود است!");
                return View(role);
            }

            Role.RoleTitle = role.RoleTitle;
            roleService.AddPermmisionToRole(role.PermmisionsId, Role.RoleId);
            await roleService.UpdateRole(Role);

            return RedirectToAction("Index", new { message = "ویرایش با موفقیت انجام شد" });
        }

        [PermissionChecker(19)]
        // GET: Admin/Roles/Delete/5
        public async Task<IActionResult> Delete(int? roleId)
        {
            if (roleId == null)
            {
                return NotFound();
            }

            var role = await roleService.GetRoleById((int)roleId);

            if (role == null)
            {
                return NotFound();
            }

            await roleService.DeleteRole(role);
            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }
    }
}

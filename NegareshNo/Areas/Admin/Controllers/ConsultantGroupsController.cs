using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegareshNo.Core.Securities;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Data.Model.Consulting;

namespace NegareshNo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionChecker(8)]
    public class ConsultantGroupsController : Controller
    {
        private readonly IConsultantGroupService consultantGroupService;

        public ConsultantGroupsController(IConsultantGroupService consultantGroupService) => this.consultantGroupService = consultantGroupService;

        [PermissionChecker(8)]
        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;
            return View(await consultantGroupService.GetAllGroupForAdmin());
        }

        [HttpGet]
        [PermissionChecker(9)]
        public IActionResult Create() => View();
     
        [HttpPost]
        [PermissionChecker(9)]
        public async Task<IActionResult> Create(ConsultingGroup consultantGroups,IFormFile ImageFile)
        {
            if (!ModelState.IsValid) return View(consultantGroups);

            if(await consultantGroupService.IsGroupTitleExist(consultantGroups.GroupTitle))
            {
                ModelState.AddModelError("GroupTitle", "گروهی با این عنوان موجود هست");
                return View(consultantGroups);
            }

            await consultantGroupService.AddGroup(consultantGroups, ImageFile);

            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }

        [HttpGet]
        [PermissionChecker(10)]
        public async Task<IActionResult> Edit(int? groupId)
        {
            if (groupId == null) return NotFound();

            var group = await consultantGroupService.GetGroupById((int)groupId);

            if (group == null) return NotFound();

            return View(group);
        }

        [HttpPost]
        [PermissionChecker(10)]
        public async Task<IActionResult> Edit(ConsultingGroup consultingGroup,IFormFile ImageFile)
        {
            if (!ModelState.IsValid) return View(consultingGroup);

            var groupTitle = await consultantGroupService.GetGroupById(consultingGroup.GroupId);

            if (consultingGroup.GroupTitle != groupTitle.GroupTitle  && await consultantGroupService.IsGroupTitleExist(consultingGroup.GroupTitle))
            {
                ModelState.AddModelError("GroupTitle", "گروهی با این عنوان موجود هست");
                return View(consultingGroup);
            }

            await consultantGroupService.UpdateGroup(consultingGroup, ImageFile);

            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }

        [PermissionChecker(11)]
        public async Task<IActionResult> Delete(int? groupId)
        {
            if (groupId == null) return NotFound();

            await consultantGroupService.DeleteGroup((int)groupId);

            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }
    }
}
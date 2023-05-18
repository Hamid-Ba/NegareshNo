using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.Consultant;
using NegareshNo.Data.Model.Consulting;

namespace NegareshNo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ConsultantAndGroupsController : Controller
    {
        private readonly IConsultantAndGroupService consultantAndGroupService;
        private readonly IConsultantService consultantService;
        private readonly IConsultantGroupService consultantGroupService;

        public ConsultantAndGroupsController(IConsultantAndGroupService consultantAndGroupService, IConsultantService consultantService, IConsultantGroupService consultantGroupService)
        {
            this.consultantAndGroupService = consultantAndGroupService;
            this.consultantGroupService = consultantGroupService;
            this.consultantService = consultantService;
        }

        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;
            return View(await consultantAndGroupService.GetAllConsultantWithHisGroups());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Consultants = new SelectList(await consultantService.GetJustConsultant(), "ConsultantId", "ConsultantFullName");
            ViewBag.Groups = new SelectList(await consultantGroupService.GetAllGroupForAdmin(), "GroupId", "GroupTitle");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int consultantId, int[] GroupsId)
        {
            ViewBag.Consultants = new SelectList(await consultantService.GetJustConsultant(), "ConsultantId", "ConsultantFullName");
            ViewBag.Groups = new SelectList(await consultantGroupService.GetAllGroupForAdmin(), "GroupId", "GroupTitle");

            if (!ModelState.IsValid) return View();

            if (!await consultantAndGroupService.CanCreateResponsibility(consultantId)) { return RedirectToAction("Index", new { message = "این مشاور دارای مسئولیت می باشد" }); }

            await consultantAndGroupService.AddResponsibilitesToConsultant(consultantId, GroupsId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? consultantId)
        {
            if (consultantId == null) return NotFound();

            ViewBag.Consultants = new SelectList(await consultantService.GetJustConsultant(), "ConsultantId", "ConsultantFullName");
            ViewBag.Groups = new SelectList(await consultantGroupService.GetAllGroupForAdmin(), "GroupId", "GroupTitle");

            return View(await consultantAndGroupService.GetResponsibilityForEdit((int)consultantId));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ConsultantAndGroupEditVM ViewModel)
        {
            ViewBag.Consultants = new SelectList(await consultantService.GetJustConsultant(), "ConsultantId", "ConsultantFullName");
            ViewBag.Groups = new SelectList(await consultantGroupService.GetAllGroupForAdmin(), "GroupId", "GroupTitle");

            if (!ModelState.IsValid) return View(ViewModel);

            await consultantAndGroupService.AddResponsibilitesToConsultant(ViewModel.ConsultantId, ViewModel.GroupsId);
            return RedirectToAction("Index", new { message = "ویرایش با موفقیت انجام شد" });
        }

        public async Task<IActionResult> Delete(int? consultantId)
        {
            if (consultantId == null)
            {
                return NotFound();
            }

            await consultantAndGroupService.DeleteConsultantResponsibilites((int)consultantId);
            return RedirectToAction("Index", new { message = "حذف با موفقیت انجام شد" });
        }
    }
}
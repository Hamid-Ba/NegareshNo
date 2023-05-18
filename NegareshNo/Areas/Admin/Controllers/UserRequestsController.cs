using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NegareshNo.Core.Securities;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Core.ViewModels.User;
using Newtonsoft.Json;
using ReflectionIT.Mvc.Paging;

namespace NegareshNo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserRequestsController : Controller
    {
        private readonly IUserRequestService userRequestService;
        private readonly IConsultantService consultantService;
        private readonly IConsultantGroupService consultantGroupService;
        private readonly IConsultantAndGroupService consultantAndGroupService;

        public UserRequestsController(IUserRequestService userRequestService, IConsultantGroupService consultantGroupService, IConsultantService consultantService,
            IConsultantAndGroupService consultantAndGroupService)
        {
            this.userRequestService = userRequestService;
            this.consultantGroupService = consultantGroupService;
            this.consultantService = consultantService;
            this.consultantAndGroupService = consultantAndGroupService;
        }

        [Authorize]
        [PermissionChecker(6)]
        public async Task<IActionResult> Index(string message, int page = 1)
        {
            var Model = PagingList.Create(await userRequestService.GetAllRequestForAdmin(), 10, page);
            ViewBag.RowCount = page * 10 - 9;

            ViewBag.Message = message;

            return View(Model);
        }

        [HttpGet]
        [Route("Request")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Consultants = new SelectList(consultantService.GetAllConsultantsForSite(), "ConsultantId", "ConsultantFullName");
            ViewBag.Groups = new SelectList(await consultantGroupService.GetAllGroupForAdmin(), "GroupId", "GroupTitle");

            return View("_RequestPV");
        }

        [HttpPost]
        [Route("Request")]
        public async Task<IActionResult> Create(UserRequestCreateVM ViewModel)
        {
            ViewBag.Consultants = new SelectList(consultantService.GetAllConsultantsForSite(), "ConsultantId", "ConsultantFullName");
            ViewBag.Groups = new SelectList(await consultantGroupService.GetAllGroupForAdmin(), "GroupId", "GroupTitle");

            if (!ModelState.IsValid) return View("_RequestPV", ViewModel);

            await userRequestService.AddRequest(ViewModel);

            return RedirectToAction("Index", "Home", new { message = "درخواست شما با موفقیت ثبت شد ، به زودی با شما تماس گرفته خواهد شد" });
        }
        
        [HttpGet]
        //[Route("U/{id?}")]
        public async Task<IActionResult> UpdateConsultantName(int? id)
        {
            var consultants = await consultantAndGroupService.GetGroupsConsultantById((int)id);
            return Json(JsonConvert.SerializeObject(consultants));
        }

        [HttpGet]
        [Authorize]
        [PermissionChecker(7)]
        public async Task<IActionResult> Edit(int? requestId)
        {
            if (requestId == null) return NotFound();

            var request = await userRequestService.GetRequestForUpdate((int)requestId);

            if (request == null) return NotFound();

            ViewBag.Consultants = new SelectList(await consultantService.GetAllConsultantsIndexForAdminPanel(), "ConsultantId", "ConsultantFullName");
            return View(request);
        }

        [HttpPost]
        [Authorize]
        [PermissionChecker(7)]
        public async Task<IActionResult> Edit(UserRequestEditVM ViewModel)
        {
            ViewBag.Consultants = new SelectList(await consultantService.GetAllConsultantsIndexForAdminPanel(), "ConsultantId", "ConsultantFullName");
            if (!ModelState.IsValid) return View(ViewModel);

            if (ViewModel.HasTime && ViewModel.GivenTime.ToString() == "1/1/0001 12:00:00 AM")
            {
                ModelState.AddModelError("GivenTime", "اگر زمان را تنظیم کرده اید ، تیک را هم اعمال کنید");
                return View(ViewModel);
            }

            await userRequestService.UpdateRequest(ViewModel);

            return RedirectToAction("Index", new { message = "تغییرات با موفقیت اعمال شد" });
        }

        [Authorize]
        [PermissionChecker(7)]
        public async Task<IActionResult> DoneRequest(int? requestId)
        {
            if (requestId == null) return NotFound();

            await userRequestService.MakeDoneRequest((int)requestId);

            return RedirectToAction("Index", new { message = "تغییرات با موفقیت اعمال شد" });
        }

        [Authorize]
        [PermissionChecker(7)]
        public async Task<IActionResult> Delete(int? requestId)
        {
            if (requestId == null) return NotFound();

            await userRequestService.DeleteRequest((int)requestId);

            return RedirectToAction("Index", new { message = "حذف با موفقیت اعمال شد" });
        }

    }
}
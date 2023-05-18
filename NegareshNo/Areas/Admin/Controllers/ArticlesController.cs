using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NegareshNo.Core.Securities;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Data.Model.Consulting;
using ReflectionIT.Mvc.Paging;

namespace NegareshNo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    [PermissionChecker(12)]
    public class ArticlesController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IConsultantService consultantService;

        public ArticlesController(IArticleService articleService, IConsultantService consultantService)
        {
            this.articleService = articleService;
            this.consultantService = consultantService;
        }

        [PermissionChecker(12)]
        public async Task<IActionResult> Index(string message,int page = 1)
        {
            var pageModel = PagingList.Create(await articleService.GetAllArticleForAdmin(), 7, page);
            ViewBag.RowCount = page * 7 - 6;
            ViewBag.Message = message;

            return View(pageModel);
        }

        [HttpGet]
        [PermissionChecker(13)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Writers = new SelectList(await consultantService.GetAllConsultantsIndexForAdminPanel(), "ConsultantId", "ConsultantFullName");
            return View();
        }

        [HttpPost]
        [PermissionChecker(13)]
        public async Task<IActionResult> Create(Article artilce)
        {
            ViewBag.Writers = new SelectList(await consultantService.GetAllConsultantsIndexForAdminPanel(), "ConsultantId", "ConsultantFullName");

            if (!ModelState.IsValid) return View(artilce);

            await articleService.AddArticle(artilce);

            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }

        [PermissionChecker(12)]
        public async Task<IActionResult> Details(int? articleId)
        {
            if (articleId == null) return NotFound();

            var article = await articleService.GetArticleById((int)articleId);

            if (article == null) return NotFound();

            ViewBag.Writer = await articleService.GetWriterNameOfArticle(article.ConsultingId);

            return View(article);
        }

        [HttpGet]
        [PermissionChecker(14)]
        public async Task<IActionResult> Edit(int? articleId)
        {
            if (articleId == null) return NotFound();

            var article = await articleService.GetArticleById((int)articleId);

            if (article == null) return NotFound();

            ViewBag.Writers = new SelectList(await consultantService.GetAllConsultantsIndexForAdminPanel(), "ConsultantId", "ConsultantFullName");
            return View(article);
        }

        [HttpPost]
        [PermissionChecker(14)]
        public async Task<IActionResult> Edit(Article article)
        {
            ViewBag.Writers = new SelectList(await consultantService.GetAllConsultantsIndexForAdminPanel(), "ConsultantId", "ConsultantFullName");
            if (!ModelState.IsValid) return NotFound();

            await articleService.UpdateArticle(article);

            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }

        [PermissionChecker(15)]
        public async Task<IActionResult> Delete(int? articleId)
        {
            if (articleId == null) return NotFound();

            await articleService.DeleteArticle((int)articleId);

            return RedirectToAction("Index", new { message = "عملیات با موفقیت انجام شد" });
        }

        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/image/ArticleImage",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);
            }

            var url = $"{"/image/ArticleImage/"}{fileName}";

            return Json(new { uploaded = true, url });
        }
    }
}
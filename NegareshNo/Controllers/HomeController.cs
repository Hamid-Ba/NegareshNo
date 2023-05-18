using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NegareshNo.Core.Services.ADT;

namespace NegareshNo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConsultantService consultantService;
        private readonly IConsultantGroupService consultantGroupService;
        private readonly IArticleService articleService;

        public HomeController(IConsultantGroupService consultantGroupService, IArticleService articleService, IConsultantService consultantService)
        {
            this.consultantGroupService = consultantGroupService;
            this.articleService = articleService;
            this.consultantService = consultantService;
        }

        public async Task<IActionResult> Index(string message)
        {
            ViewBag.Message = message;
            ViewBag.Consultants = consultantService.GetAllConsultantsForSite();
            ViewBag.Groups = await consultantGroupService.GetAllGroupForSite();
            ViewBag.Articles = await articleService.GetAllArticleForSite();
            return View();
        }

        [Route("Article/{articleId?}")]
        public async Task<IActionResult> Article(int? articleId)
        {
            if (articleId == null) return NotFound();

            var article = await articleService.GetArticleById((int)articleId);
            if (article == null) return NotFound();

            ViewBag.Writer = await articleService.GetWriterNameOfArticleById((int)articleId);
            ViewBag.Articles = await articleService.GetAllArticleForSite();
            return View(article);
        }

        [Route("Consultant/{consultantId?}")]
        public async Task<IActionResult> Consultant(int? consultantId)
        {
            if (consultantId == null) return NotFound();

            var consultant = await consultantService.GetConsultantById((int)consultantId);
            if (consultant == null) return NotFound();

            return View(consultant);
        }

        [Route("ContactUs")]
        public IActionResult Contact() => View("ContactUs");

        [Route("AboutUs")]
        public IActionResult AboutUs() => View();

    }
}

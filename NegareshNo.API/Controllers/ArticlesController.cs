using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegareshNo.Core.Services.ADT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NegareshNo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {

        private readonly IArticleService articleService;

        public ArticlesController(IArticleService articleService) => this.articleService = articleService;

        [HttpGet]
        public async Task<IActionResult> GetArticle()
        {
            var res = new ObjectResult(await articleService.GetAllArticleForSite())
            {
                StatusCode = (int)HttpStatusCode.OK
            };

            return res;
        }


    }
}

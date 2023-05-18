using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegareshNo.Core.Services.ADT;
using NegareshNo.Data.Model.Consulting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NegareshNo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantController : ControllerBase
    {
        private readonly IConsultantService consultantService;

        public ConsultantController(IConsultantService consultantService) => this.consultantService = consultantService;

        [HttpGet]
        [ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetConsultant()
        {
            var res = await consultantService.GetAllConsultantsIndexForAdminPanel();

            Request.HttpContext.Response.Headers.Add("X-Count", res.Count().ToString());
            Request.HttpContext.Response.Headers.Add("X-Programmer", "HMD");

            return new ObjectResult(res);
        }

        [HttpGet("{consultantId}")]
        public async Task<IActionResult> GetConsultant([FromRoute] int consultantId)
        {
            if (await consultantService.IsExistConsultant(consultantId))
            {
                var consultant = await consultantService.GetConsultantById(consultantId);

                return Ok(consultant);
            }

            else return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> PostConsultant([FromBody] Consultant consultant)
        {
            if (!ModelState.IsValid) return BadRequest(consultant);

            var res = await consultantService.CreateConsultant(consultant, null,consultant.Role_Consultants);

            return CreatedAtAction("GetConsultant",new { consultantId = res }, consultant);
        }

    }
}

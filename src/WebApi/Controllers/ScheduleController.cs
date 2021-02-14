using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    public class ScheduleController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetSchedule(int year, int month)
        {
            return Ok();
        }
    }
}

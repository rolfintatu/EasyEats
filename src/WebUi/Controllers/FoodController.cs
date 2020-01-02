using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FoodController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            var stringList = new string[] { "one", "two" };
            return Ok(stringList);
        }
    }
}
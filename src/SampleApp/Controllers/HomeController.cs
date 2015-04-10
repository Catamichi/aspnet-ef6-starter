using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNet.Mvc;

using SampleApp.Entities;
using SampleApp.ViewModels;

namespace SampleApp
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            var model = new HomeIndexModel
            {
                Message = "Hello, world!"
            };
            
            return View(model);
        }
    }
}
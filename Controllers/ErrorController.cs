using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Emp.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statuscode}")]
        public IActionResult HttpStatusCodeHandler(int statuscode)
        {
            switch(statuscode)
            {
                case 404:
                    ViewData["ErrorMessage"] = "Sorry, the resource requested could not be resolved";
                    break;
            }
            return View("NotFound");
        }
    }
}
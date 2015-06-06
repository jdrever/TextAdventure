using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TextAdventure.MVC.Models;

namespace TextAdventure.MVC.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult UserInput()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateInput(UserInput userInput)
        {
            return Content(userInput.ToString());
        }
    }
}
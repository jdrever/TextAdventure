using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TextAdventure.Interface;
using TextAdventure.MVC.Models;

namespace TextAdventure.MVC.Controllers
{
    public class HomeController : Controller
    {
        private IParser _parser;

        public HomeController(IParser parser)
        {
            if (parser == null) throw new ArgumentNullException("parser");
            _parser = parser;
        }

        public ActionResult UserInput()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateInput(UserInput userInput)
        {
            string userInputText = userInput.InputText;
            return Content(_parser.ParseInput(userInput.ToString()));
        }
    }
}
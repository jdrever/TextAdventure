using System;
using System.Web.Mvc;
using TextAdventure.Interface;

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


        public ActionResult CreateInput()
        {
            return View();
        }
        //[HttpPost]
        //public ActionResult CreateInput(UserInput userInput)
        //{
        //    string userInputText = userInput.InputText;
        //    return Content(_parser.ParseInput(userInput.ToString()));
        //}
    }
}
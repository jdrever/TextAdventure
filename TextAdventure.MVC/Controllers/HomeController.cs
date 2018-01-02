using System;
using System.Web.Mvc;
using TextAdventure.Domain;
using TextAdventure.Interface;

namespace TextAdventure.MVC.Controllers
{
    public class HomeController : Controller
    {
        private IParser _parser;
        private CharacterLocationDetails details;

        public HomeController(IParser parser)
        {
            if (parser == null) throw new ArgumentNullException("parser");
            _parser = parser;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SubmitInput(string inputText)
        {
            //TODO: need to keep track of the charcter's GUID
            _parser.ParseInput(details, inputText);
            return View("Index");
        }
    }
}
using System.Web.Mvc;

namespace Ogretmen_Ozel.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {





            return View();
        }

        public ActionResult Subjects()
        {
            ViewBag.Message = "Your application description pagefffffffffffffffffff.";

            return View();
        }


    }
}
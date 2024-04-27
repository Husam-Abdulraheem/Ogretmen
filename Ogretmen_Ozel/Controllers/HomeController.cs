using Ogretmen_Ozel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ogretmen_Ozel.Controllers
{
    public class HomeController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TeachersPage(int? address)
        {
            List<Teacher> vm = new List<Teacher>();

            if (address == 1)
            {
                vm = db.TeachersTable.Where(x => x.User.Address.Country == "Türkiye").ToList();

            }


            return View(vm);
        }


    }
}
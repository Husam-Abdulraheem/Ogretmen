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
            List<Subject> listSubject = new List<Subject>();
            listSubject = db.SubjectTable.ToList();

            return View(listSubject);
        }

<<<<<<< HEAD

        [Authorize]
        public ActionResult TeachersPage(string addgiress)
=======
        public ActionResult TeachersPage(string address)
>>>>>>> parent of bcd31e7 (Ali_2_Authenction+Filtre+someChnagesOnStyle+)
        {
            List<Teacher> vm = new List<Teacher>();

            switch (address)
            {
                case "Country":
                    vm = db.TeachersTable.Where(x => x.User.Address.Country == "Türkiye").ToList();
                    break;
                case "City":
                    vm = db.TeachersTable.Where(x => x.User.Address.City == "Bartin").ToList();
                    break;
                case "Street":
                    vm = db.TeachersTable.Where(x => x.User.Address.Street == "KYK").ToList();
                    break;
                default:
                    vm = db.TeachersTable.ToList();
                    break;
            }
            return View(vm);
        }


    }
}
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


        [Authorize]
        public ActionResult TeachersPage(string address)

        {
            List<Teacher> vm = new List<Teacher>();
            //if (subject != null)
            //{
            //    vm = db.TeachersTable.Where(x => x.Subject.Id == subject).ToList();
            //}

            switch (address)
            {
                case "Country":
                    string county = (string)Session["AddressCountry"];

                    vm = db.TeachersTable.Where(x => x.User.Address.Country == county).ToList();
                    break;
                case "City":
                    string city = (string)Session["AddressCity"];
                    vm = db.TeachersTable.Where(x => x.User.Address.City == city).ToList();
                    break;
                case "Street":
                    string street = (string)Session["AddressStreet"];
                    vm = db.TeachersTable.Where(x => x.User.Address.Street == street).ToList();
                    break;
                default:
                    vm = db.TeachersTable.ToList();
                    break;
            }
            return View(vm);
        }


    }
}
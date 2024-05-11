using Ogretmen_Ozel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            if (!User.Identity.IsAuthenticated)
            {
                //HttpCookie isTeacher = new HttpCookie("IsTeacher", "false");
                HttpCookie httpCookie = new HttpCookie("isAdmin", "");
                HttpContext.Response.Cookies.Add(httpCookie);

            }
            return View(listSubject);
        }


        [Authorize]
        [HttpGet]
        public ActionResult TeachersPage(string address, int? id)

        {
            List<Teacher> vm = new List<Teacher>();
            List<Subject> listSubject = new List<Subject>();
            listSubject = db.SubjectTable.ToList();

            IEnumerable<SelectListItem> currentSubject = (from x in db.SubjectTable.ToList()
                                                          select new SelectListItem()
                                                          {
                                                              Text = x.SubjectName,
                                                              Value = x.Id.ToString()
                                                          }).ToList();

            ViewData["AllSubject"] = currentSubject;

            int userId;
            string name = User.Identity.Name.ToString();
            int.TryParse(name, out userId);

            User currentUser = db.UserTable.Where(x => x.Id == userId).FirstOrDefault();
            string county = currentUser.Address.Country;
            string city = currentUser.Address.City;
            string street = currentUser.Address.Street;

            if (id != null && address != "")
            {

                switch (address)
                {
                    case "Country":
                        vm = db.TeachersTable.Where(x => x.User.Address.Country == county && x.Subject.Id == id).ToList();
                        break;
                    case "City":

                        vm = db.TeachersTable.Where(x => x.User.Address.City == city && x.Subject.Id == id).ToList();
                        break;
                    case "Street":

                        vm = db.TeachersTable.Where(x => x.User.Address.Street == street && x.Subject.Id == id).ToList();
                        break;
                    default:
                        vm = db.TeachersTable.ToList();
                        break;
                }
            }
            else if (id != null && address == "")
            {
                vm = db.TeachersTable.Where(x => x.Subject.Id == id).ToList();

            }
            else
            {
                switch (address)
                {
                    case "Country":
                        vm = db.TeachersTable.Where(x => x.User.Address.Country == county).ToList();
                        break;
                    case "City":

                        vm = db.TeachersTable.Where(x => x.User.Address.City == city).ToList();
                        break;
                    case "Street":

                        vm = db.TeachersTable.Where(x => x.User.Address.Street == street).ToList();
                        break;
                    default:
                        vm = db.TeachersTable.ToList();
                        break;
                }
            }


            return View(vm);
        }


    }
}
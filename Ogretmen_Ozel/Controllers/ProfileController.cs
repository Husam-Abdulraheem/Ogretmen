using Ogretmen_Ozel.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ogretmen_Ozel.Controllers
{
    public class ProfileController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        [HttpGet]
        public ActionResult Profile(int? userid)
        {
            Teacher teacher = new Teacher();


            if (userid != null)
            {
                teacher = db.TeachersTable.Where(x => x.Id == userid).FirstOrDefault();
                ViewBag.userid = userid;
            }


            return View(teacher);
        }

        [HttpPost]
        public ActionResult Profile(int? userid, Teacher ChangeAddress)
        {
            Teacher addressUpdateInDb = db.TeachersTable.Where(x => x.Id == userid).FirstOrDefault();

            if (addressUpdateInDb != null)
            {
                addressUpdateInDb.User.Address.Country = ChangeAddress.User.Address.Country;
                addressUpdateInDb.User.Address.City = ChangeAddress.User.Address.City;
                addressUpdateInDb.User.Address.Street = ChangeAddress.User.Address.Street;
                addressUpdateInDb.Description = ChangeAddress.Description;

                db.SaveChanges();
            }
            return View(addressUpdateInDb);
        }
    }
}
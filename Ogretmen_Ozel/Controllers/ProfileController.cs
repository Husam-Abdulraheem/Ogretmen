using Ogretmen_Ozel.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ogretmen_Ozel.Controllers
{
    public class ProfileController : Controller
    {
        DataBaseContext db = new DataBaseContext();


        [Authorize]
        [HttpGet]
        public ActionResult Profile(int? userId)
        {
            Teacher teacher = new Teacher();
            if (userId != null)
            {
                teacher = db.TeachersTable.Where(x => x.Id == userId).FirstOrDefault();

                return View(teacher);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Profile(int? userId, Teacher ChangeAddress)
        {
            Teacher addressUpdateInDb = db.TeachersTable.Where(x => x.Id == userId).FirstOrDefault();
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
using Ogretmen_Ozel.Models;
using System.Linq;
using System.Web.Mvc;

namespace Ogretmen_Ozel.Controllers
{
    public class ProfileController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        // GET: Profile
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
    }
}
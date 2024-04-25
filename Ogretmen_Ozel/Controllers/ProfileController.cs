using Ogretmen_Ozel.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Ogretmen_Ozel.Controllers
{
    public class ProfileController : Controller
    {
        DataBaseContext db = new DataBaseContext();
        // GET: Profile
        public ActionResult Profile()
        {


            List<Teacher> Teachers;
            Teachers = db.TeachersTable.ToList();
            Teacher teach = Teachers.First();


            return View(Teachers);
        }
    }
}
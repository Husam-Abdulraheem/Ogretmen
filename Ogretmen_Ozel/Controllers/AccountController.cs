using Ogretmen_Ozel.Models;
using Ogretmen_Ozel.Models.AccountModels;
using System.Linq;
using System.Web.Mvc;

public class AccountController : Controller
{
    DataBaseContext db = new DataBaseContext();
    public ActionResult LogIn()
    {
        return View();
    }
    [HttpGet]
    public ActionResult SignUp_Teacher()
    {
        TeacherSignUp teacherSignUp = new TeacherSignUp();
        teacherSignUp.Subject = db.SubjectTable.ToList();
        return View(teacherSignUp);
    }

    [HttpPost]
    public ActionResult SignUp_Teacher(TeacherSignUp teacherSignUp)
    {

        db.AddressTable.Add(teacherSignUp.Address);
        teacherSignUp.User.IsTeacher = true;
        teacherSignUp.User.Address = teacherSignUp.Address;


        db.UserTable.Add(teacherSignUp.User);
        int result = db.SaveChanges();
        if (result > 0)
        {
            ViewBag.result = "Saved";
        }
        else
        {
            ViewBag.result = "Do not Save";
        }


        return View();
    }
}
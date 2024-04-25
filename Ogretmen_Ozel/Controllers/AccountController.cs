using Ogretmen_Ozel.Models;
using Ogretmen_Ozel.Models.AccountModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

public class AccountController : Controller
{
    public ActionResult LogIn()
    {
        return View();
    }


    DataBaseContext db = new DataBaseContext();
    [HttpGet]
    public ActionResult SignUp_Teacher()
    {
        TeacherSignUp teacherSignUp = new TeacherSignUp();
        // teacherSignUp.Subject = new Subject();
        IEnumerable<SelectListItem> SubjectLists = (from x in db.SubjectTable.ToList()
                                                    select new SelectListItem()
                                                    {
                                                        Text = x.SubjectName,
                                                        Value = x.Id.ToString()
                                                    }).ToList();

        ViewData["Subject"] = SubjectLists;

        return View(teacherSignUp);
    }

    [HttpPost]
    public ActionResult SignUp_Teacher(TeacherSignUp teacherSignUp)
    {
        Teacher teacher = new Teacher();
        db.AddressTable.Add(teacherSignUp.Address);
        teacherSignUp.User.IsTeacher = true;
        teacherSignUp.User.Address = teacherSignUp.Address;
        db.UserTable.Add(teacherSignUp.User);

        teacher.User = teacherSignUp.User;
        teacher.SubjectName = db.SubjectTable.Find(teacherSignUp.Id);

        db.TeachersTable.Add(teacher);


        int result = db.SaveChanges();
        if (result > 0)
        {
            ViewBag.result = "Saved";
        }
        else
        {
            ViewBag.result = "Do not Save";
        }

        IEnumerable<SelectListItem> SubjectLists = (from x in db.SubjectTable.ToList()
                                                    select new SelectListItem()
                                                    {
                                                        Text = x.SubjectName,
                                                        Value = x.Id.ToString()
                                                    }).ToList();

        ViewData["Subject"] = SubjectLists;
        return View();
    }
}
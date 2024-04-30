using Ogretmen_Ozel.Models;
using Ogretmen_Ozel.Models.AccountModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;

public class AccountController : Controller
{
    DataBaseContext db = new DataBaseContext();
    //Login for all users
    public ActionResult LogIn()
    {
        return View();
    }
    [HttpPost]
    public ActionResult LogIn(User user)
    {
        User loginUser = db.UserTable.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();

        if (loginUser != null)
        {
            Session["SessionUser"] = user.Name;
            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.login = "Hata oldu";
            return View();
        }

    }


    //signUp for teacher
    [HttpGet]
    public ActionResult SignUp_Teacher()
    {
        TeacherSignUp teacherSignUp = new TeacherSignUp();

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
        if (ModelState.IsValid)
        {
            Teacher teacher = new Teacher();
            teacherSignUp.User.IsTeacher = true;
            teacherSignUp.User.Address = teacherSignUp.Address;
            db.AddressTable.Add(teacherSignUp.Address);
            db.UserTable.Add(teacherSignUp.User);

            teacher.User = teacherSignUp.User;
            teacher.Subject = db.SubjectTable.Find(teacherSignUp.Id);

            db.TeachersTable.Add(teacher);
            int result = db.SaveChanges();
            if (result > 0)
                return RedirectToAction("Index", "Home");
        }




        IEnumerable<SelectListItem> SubjectLists = (from x in db.SubjectTable.ToList()
                                                    select new SelectListItem()
                                                    {
                                                        Text = x.SubjectName,
                                                        Value = x.Id.ToString()
                                                    }).ToList();

        ViewBag.resultT = "Giriş yaparken bir hata oluştu";
        ViewData["Subject"] = SubjectLists;
        return View();
    }


    //signUp for Student
    [HttpGet]
    public ActionResult SignUp_Student()
    {

        return View();
    }

    [HttpPost]
    public ActionResult SignUp_Student(StudentSignUp studentSignUp)
    {
        if (ModelState.IsValid)
        {
            Student student = new Student();
            studentSignUp.User.IsTeacher = false;
            studentSignUp.User.Address = studentSignUp.Address;
            db.AddressTable.Add(studentSignUp.Address);
            db.UserTable.Add(studentSignUp.User);

            student.User = studentSignUp.User;
            db.StudentTable.Add(student);
            int result = db.SaveChanges();

            if (result > 0)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        ViewBag.resultS = "Giriş yaparken bir hata oluştu";
        return View();
    }

}
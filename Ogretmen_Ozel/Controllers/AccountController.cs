using Ogretmen_Ozel.Models;
using Ogretmen_Ozel.Models.AccountModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

public class AccountController : Controller
{
    DataBaseContext db = new DataBaseContext();
    //Login for all users
    public ActionResult LogIn()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }
        //string userLogin = se
        return View();
    }
    [HttpPost]
    public ActionResult LogIn(User user)
    {
        User loginUser = db.UserTable.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();

        if (loginUser != null)
        {
            string currentUser = string.Format("{0}|{1}|{2}|{3}", loginUser.Id, loginUser.Address.Country, loginUser.Address.City, loginUser.Address.Street);
            /* Session["isLoging"] = true;
             Session["AddressCountry"] = loginUser.Address.Country;
             Session["AddressCity"] = loginUser.Address.City;
             Session["AddressStreet"] = loginUser.Address.Street;*/
            if (loginUser.IsTeacher == true)
            {
                //

                FormsAuthentication.SetAuthCookie(currentUser, false);

                return RedirectToAction("Profile", "Profile", new { userId = loginUser.Id });

            }

            else
                return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.login = "Hata oldu";
            return View();
        }

    }

    [Authorize]
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
            User UserUnique = db.UserTable.Where(x => x.Email == teacherSignUp.User.Email).FirstOrDefault();
            if (UserUnique == null)
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
            else
            {
                IEnumerable<SelectListItem> SubjectLists1 = (from x in db.SubjectTable.ToList()
                                                             select new SelectListItem()
                                                             {
                                                                 Text = x.SubjectName,
                                                                 Value = x.Id.ToString()
                                                             }).ToList();

                ViewBag.resultT = "Girdiğiniz email alınmıştır";
                ViewData["Subject"] = SubjectLists1;
                return View();
            }

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


    [Authorize]
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


    [Authorize]
    public ActionResult logout()
    {

        FormsAuthentication.SignOut();
        return RedirectToAction("Index", "Home");
    }

}
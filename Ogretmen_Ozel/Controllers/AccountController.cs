using Ogretmen_Ozel.Models;
using Ogretmen_Ozel.Models.AccountModels;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
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
        return View();
    }

    [HttpPost]
    public ActionResult LogIn(User user)
    {
        User loginUser = db.UserTable.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();

        if (loginUser != null)
        {



            FormsAuthentication.SetAuthCookie(loginUser.Id.ToString(), false);



            return RedirectToAction("Index", "Home");
        }
        else
        {
            ViewBag.login = "Hatalı Email veya parola!";
            return View();
        }

    }

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
    public ActionResult SignUp_Teacher(TeacherSignUp teacherSignUp, HttpPostedFileBase imageFile)
    {
        if (ModelState.IsValid)
        {



            User UserUnique = db.UserTable.Where(x => x.Email == teacherSignUp.User.Email).FirstOrDefault();
            if (UserUnique == null)
            {





                Teacher teacher = new Teacher();
                if (imageFile != null)
                {
                    if (imageFile.FileName.EndsWith("png") || imageFile.FileName.EndsWith("jpg") || imageFile.FileName.EndsWith("svg") || imageFile.FileName.EndsWith("jpeg"))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                        string ex = Path.GetExtension(imageFile.FileName);
                        fileName = fileName + ex;
                        teacherSignUp.User.Image = "../Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("../Images/"), fileName);
                        imageFile.SaveAs(fileName);
                    }
                }



                teacherSignUp.User.IsTeacher = true;

                teacherSignUp.User.Address = teacherSignUp.Address;
                db.AddressTable.Add(teacherSignUp.Address);
                db.UserTable.Add(teacherSignUp.User);

                teacher.User = teacherSignUp.User;
                teacher.Subject = db.SubjectTable.Find(teacherSignUp.Id);

                db.TeachersTable.Add(teacher);
                int result = db.SaveChanges();
                if (result > 0)
                    return RedirectToAction("LogIn", "Account");
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




    [HttpGet]
    public ActionResult SignUp_Student()
    {

        return View();
    }



    [HttpPost]
    public ActionResult SignUp_Student(StudentSignUp studentSignUp, HttpPostedFileBase imageFile)
    {
        if (ModelState.IsValid)
        {
            User UserUnique = db.UserTable.Where(x => x.Email == studentSignUp.User.Email).FirstOrDefault();
            if (UserUnique == null)
            {
                Student student = new Student();
                if (imageFile != null)
                {
                    if (imageFile.FileName.EndsWith("png") || imageFile.FileName.EndsWith("jpg") || imageFile.FileName.EndsWith("svg") || imageFile.FileName.EndsWith("jpeg"))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                        string ex = Path.GetExtension(imageFile.FileName);

                        fileName = fileName + ex;
                        studentSignUp.User.Image = "../Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("../Images/"), fileName);
                        imageFile.SaveAs(fileName);
                    }
                }



                studentSignUp.User.IsTeacher = false;

                studentSignUp.User.Address = studentSignUp.Address;
                db.AddressTable.Add(studentSignUp.Address);
                db.UserTable.Add(studentSignUp.User);

                student.User = studentSignUp.User;
                db.StudentTable.Add(student);
                int result = db.SaveChanges();

                if (result > 0)
                {
                    return RedirectToAction("LogIn", "Account");
                }

                else
                {
                    ViewBag.resultS = "Giriş yaparken bir hata oluştu";
                    return View();
                }
            }
        }

        ViewBag.resultS = "Girdiğiniz email alınmıştır";
        return View();
    }


    [Authorize]
    public ActionResult logout()
    {


        FormsAuthentication.SignOut();
        return RedirectToAction("Index", "Home");
    }

}
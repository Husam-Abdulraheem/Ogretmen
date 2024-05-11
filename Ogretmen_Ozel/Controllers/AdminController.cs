using Ogretmen_Ozel.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ogretmen_Ozel.Controllers
{
    public class AdminController : Controller
    {
        DataBaseContext db = new DataBaseContext();



        [Authorize]
        [HttpGet]
        public ActionResult AdminPage()
        {

            ViewBag.students = db.StudentTable.ToList();
            ViewBag.teachers = db.TeachersTable.ToList();
            ViewBag.users = db.UserTable.ToList();
            ViewBag.subjects = db.SubjectTable.ToList();
            ViewBag.reservations = db.ReservationsTable.ToList();

            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult AdminPage(Subject subject, HttpPostedFileBase imageFile)
        {
            int userId;
            string nameU = User.Identity.Name.ToString();
            int.TryParse(nameU, out userId);
            if (db.UserTable.Find(userId).IsAdmin == true)
            {

                ViewBag.students = db.StudentTable.ToList();
                ViewBag.teachers = db.TeachersTable.ToList();
                ViewBag.users = db.UserTable.ToList();
                ViewBag.subjects = db.SubjectTable.ToList();
                ViewBag.reservations = db.ReservationsTable.ToList();
                Subject addSubjects = new Subject();


                if (imageFile != null)
                {
                    if (imageFile.FileName.EndsWith("png") || imageFile.FileName.EndsWith("jpg") || imageFile.FileName.EndsWith("svg") || imageFile.FileName.EndsWith("jpeg"))
                    {

                        string fileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
                        string ex = Path.GetExtension(imageFile.FileName);
                        fileName = fileName + ex;
                        subject.Image = "../Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("../Images/"), fileName);
                        imageFile.SaveAs(fileName);
                    }
                }




                addSubjects = db.SubjectTable.Add(subject);
                db.SaveChanges();
                return RedirectToAction("AdminPage", "Admin");

            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [Authorize]
        [HttpGet]
        public ActionResult EditSubject(int sId, string name, string des)
        {
            int userId;
            string nameU = User.Identity.Name.ToString();
            int.TryParse(nameU, out userId);
            if (db.UserTable.Find(userId).IsAdmin == true)
            {
                Subject subject = db.SubjectTable.Find(sId);
                subject.SubjectName = name;
                subject.SubjectDescription = des;
                db.SaveChanges();

                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteSubject(int sId)
        {
            int userId;
            string nameU = User.Identity.Name.ToString();
            int.TryParse(nameU, out userId);
            if (db.UserTable.Find(userId).IsAdmin == true)
            {
                Subject subject = db.SubjectTable.Find(sId);
                List<Teacher> currentTeacher = db.TeachersTable.Where(x => x.Subject.Id == subject.Id).ToList();
                List<Reservation> currentReservations = db.ReservationsTable.Where(x => x.Subject.Id == subject.Id).ToList();
                foreach (Reservation r in currentReservations)
                {
                    db.ReservationsTable.Remove(r);
                }
                foreach (Teacher t in currentTeacher)
                {
                    t.Subject = null;
                }

                db.SubjectTable.Remove(subject);
                db.SaveChanges();
                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [Authorize]
        [HttpGet]
        public ActionResult DeleteStudent(int? sId)
        {
            int userId;
            string nameU = User.Identity.Name.ToString();
            int.TryParse(nameU, out userId);
            if (db.UserTable.Find(userId).IsAdmin == true)
            {
                User currentUser = db.UserTable.Where(x => x.Id == sId).FirstOrDefault();

                db.ReservationsTable.RemoveRange(db.ReservationsTable.Where(x => x.Student.User.Id == sId).ToList());
                db.StudentTable.Remove(db.StudentTable.Where(x => x.User.Id == sId).FirstOrDefault());
                db.AddressTable.Remove(currentUser.Address);
                db.UserTable.Remove(currentUser);

                db.SaveChanges();

                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteTeacher(int? sId)
        {
            int userId;
            string nameU = User.Identity.Name.ToString();
            int.TryParse(nameU, out userId);
            if (db.UserTable.Find(userId).IsAdmin == true)
            {
                User currentUser = db.UserTable.Where(x => x.Id == sId).FirstOrDefault();

                db.ReservationsTable.RemoveRange(db.ReservationsTable.Where(x => x.Teacher.User.Id == sId).ToList());
                db.TeachersTable.Remove(db.TeachersTable.Where(x => x.User.Id == sId).FirstOrDefault());
                db.AddressTable.Remove(currentUser.Address);
                db.UserTable.Remove(currentUser);

                db.SaveChanges();

                return RedirectToAction("AdminPage", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
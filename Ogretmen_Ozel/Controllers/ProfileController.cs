using Ogretmen_Ozel.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Ogretmen_Ozel.Controllers
{
    public class ProfileController : Controller
    {
        DataBaseContext db = new DataBaseContext();


        [Authorize]
        [HttpGet]
        public ActionResult TeacherProfile(int? userId)
        {
            ViewBag.submited = TempData["submited"];
            ViewBag.submited_color = TempData["submited_color"];
            if (userId != null)
            {
                User currentUser = db.UserTable.Find(userId);
                if (currentUser != null && currentUser.IsTeacher)
                {

                    Teacher teacher = new Teacher();
                    IEnumerable<SelectListItem> currentSubjects = (from x in db.SubjectTable.ToList()
                                                                   select new SelectListItem()
                                                                   {
                                                                       Text = x.SubjectName,
                                                                       Value = x.Id.ToString()
                                                                   }).ToList();
                    ViewBag.listOfSubjects = currentSubjects;

                    teacher = db.TeachersTable.Where(x => x.User.Id == userId).FirstOrDefault();

                    teacher.Reservations = db.ReservationsTable.Where(x => x.Teacher.Id == teacher.Id).ToList();
                    ViewBag.UserId = userId;

                    ViewBag.userlogin = User.Identity.Name;

                    return View(teacher);

                }
                else
                    return RedirectToAction("Index", "Home");


            }
            else
                return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public ActionResult TeacherProfile(int? userId, Teacher ChangeAddress, int? id)
        {
            Teacher addressUpdateInDb = db.TeachersTable.Where(x => x.Id == userId).FirstOrDefault();
            IEnumerable<SelectListItem> currentSubjects = (from x in db.SubjectTable.ToList()
                                                           select new SelectListItem()
                                                           {
                                                               Text = x.SubjectName,
                                                               Value = x.Id.ToString()
                                                           }).ToList();
            ViewBag.listOfSubjects = currentSubjects;
            if (id != null && addressUpdateInDb != null)
            {
                addressUpdateInDb.Subject = db.SubjectTable.Find(id);
                db.SaveChanges();
            }
            if (addressUpdateInDb != null)
            {
                addressUpdateInDb.User.Address = ChangeAddress.User.Address;
                addressUpdateInDb.Description = ChangeAddress.Description;
                addressUpdateInDb.User.Email = ChangeAddress.User.Email;
                addressUpdateInDb.User.Phone = ChangeAddress.User.Phone;
                db.SaveChanges();
            }
            return View(addressUpdateInDb);
        }


        [HttpPost]
        public ActionResult Reservation(string date, string time, int id, int studentId)
        {
            Reservation newReservation = new Reservation();
            Teacher currentTeacher = db.TeachersTable.Where(x => x.User.Id == id).FirstOrDefault();

            DateTime rTime = DateTime.ParseExact(date + "-" + time, "yyyy-MM-dd-HH:mm", CultureInfo.InvariantCulture);
            if (rTime.Year >= DateTime.Now.Year && rTime.Month >= DateTime.Now.Month && rTime.Day >= DateTime.Now.Day)
            {
                if (rTime.Day == DateTime.Now.Day)
                {

                    if (rTime.Hour > DateTime.Now.Hour && rTime.Minute > DateTime.Now.Minute)
                    {
                        newReservation.Time = rTime;

                        newReservation.Teacher = currentTeacher;
                        newReservation.Subject = currentTeacher.Subject;
                        newReservation.Student = db.StudentTable.Where(x => x.User.Id == studentId).FirstOrDefault();
                        newReservation.Status = "Beklemede";
                        db.ReservationsTable.Add(newReservation);
                        int saveDb = db.SaveChanges();
                        if (saveDb > 0)
                        {
                            TempData["submited"] = "Rezervasyon alındı";
                            TempData["submited_color"] = "bg-success";
                        }
                        else
                        {
                            TempData["submited"] = "Hata oldu !";
                            TempData["submited_color"] = "bg-danger";
                        }
                        return RedirectToAction("TeacherProfile", "Profile", new { userId = id });

                    }
                }
                else
                {
                    newReservation.Time = rTime;

                    newReservation.Teacher = currentTeacher;
                    newReservation.Subject = currentTeacher.Subject;
                    newReservation.Student = db.StudentTable.Where(x => x.User.Id == studentId).FirstOrDefault();
                    newReservation.Status = "Beklemede";
                    db.ReservationsTable.Add(newReservation);
                    int saveDb = db.SaveChanges();
                    if (saveDb > 0)
                    {
                        TempData["submited"] = "Rezervasyon alındı";
                        TempData["submited_color"] = "bg-success";
                    }
                    else
                    {
                        TempData["submited"] = "Hata oldu !";
                        TempData["submited_color"] = "bg-danger";
                    }
                    return RedirectToAction("TeacherProfile", "Profile", new { userId = id });
                }

            }

            TempData["submited"] = "Geçmişte Rezervasyon Yapamazsınız";
            TempData["submited_color"] = "bg-danger";
            return RedirectToAction("TeacherProfile", "Profile", new { userId = id });

        }

        [Authorize]
        [HttpGet]
        public ActionResult ReservationStatus(string status, int rId, int uId)
        {
            Reservation reservation = db.ReservationsTable.Find(rId);
            reservation.Status = status;
            db.SaveChanges();

            return RedirectToAction("TeacherProfile", "Profile", new { userId = uId });

        }


        [Authorize]
        [HttpGet]
        public ActionResult StudentProfile(int? userId)
        {
            ViewBag.submited = TempData["submited"];
            ViewBag.submited_color = TempData["submited_color"];

            if (userId != null)
            {
                User currentUser = db.UserTable.Find(userId);
                if (currentUser != null && !currentUser.IsTeacher)
                {
                    Student student = new Student();
                    IEnumerable<SelectListItem> currentSubjects = (from x in db.SubjectTable.ToList()
                                                                   select new SelectListItem()
                                                                   {
                                                                       Text = x.SubjectName,
                                                                       Value = x.Id.ToString()
                                                                   }).ToList();
                    ViewBag.listOfSubjects = currentSubjects;

                    student = db.StudentTable.Where(x => x.User.Id == userId).FirstOrDefault();

                    student.Reservation = db.ReservationsTable.Where(x => x.Teacher.Id == student.Id).ToList();
                    ViewBag.UserId = userId;

                    ViewBag.userlogin = User.Identity.Name;

                    return View(student);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
                return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpPost]
        public ActionResult StudentProfile(int? userId, Teacher ChangeAddress, int? id)
        {
            Student addressUpdateInDb = db.StudentTable.Where(x => x.Id == userId).FirstOrDefault();


            if (addressUpdateInDb != null)
            {
                addressUpdateInDb.User.Address = ChangeAddress.User.Address;

                addressUpdateInDb.User.Email = ChangeAddress.User.Email;
                addressUpdateInDb.User.Phone = ChangeAddress.User.Phone;
                db.SaveChanges();
            }
            return View(addressUpdateInDb);
        }

    }
}
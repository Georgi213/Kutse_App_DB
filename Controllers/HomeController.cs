using Kutse_App.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kutse_App.Controllers
{
    public class HomeController : Controller
    {
        Guest guest;
        public ActionResult Index()
        {
            string pidu = "";
            if (DateTime.Now.Month == 1)
            {
                pidu = "Head uut aastat";
            }
            else if (DateTime.Now.Month == 2)
            {
                pidu = "Iseseisvuspäeva";
            }
            else if (DateTime.Now.Month == 3)
            {
                pidu = "head rahvusvahelist naistepäeva";
            }
            else if (DateTime.Now.Month == 4)
            {
                pidu = "head kosmonautikapäeva";
            }
            else if (DateTime.Now.Month == 5)
            {
                pidu = "head võidupäeva";
            }
            else if (DateTime.Now.Month == 6)
            {
                pidu = "Head ülemaailmset vanemate päeva";
            }
            else if (DateTime.Now.Month == 7)
            {
                pidu = "head Kanada päeva";
            }
            else if (DateTime.Now.Month == 8)
            {
                pidu = "Head arbuusipäeva";
            }
            else if (DateTime.Now.Month == 9)
            {
                pidu = "Еeadmiste päev";
            }
            else if (DateTime.Now.Month == 10)
            {
                pidu = "Rahvusvahelise muusikapäevaga!";
            }
            else if (DateTime.Now.Month == 11)
            {
                pidu = "Head rahvusliku ühtsuse päeva!";
            }
            ViewBag.Message = "Ootan sind oma peole!" + pidu + "Palun tule kindlasti!";
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour < 12 && hour > 4 ? "Tere hommikust!" : "Tere päevast";
            ViewBag.Greeting = hour > 16 && hour < 20 ? "Tere õhtust!" : "Head ööd";
            ViewBag.Greeting = hour < 16 && hour > 12 ? "Tere päevast!" : "Head õhtust";
            ViewBag.Greeting = hour > 20 && hour < 4 ? "Tere ööd!" : "Head hommikust";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Tittle = "Kutse kõik";

            return View();
        }
        [HttpGet]
        public ViewResult Ankeet()
        {
            return View();
        }
        [HttpPost]
        public ViewResult Ankeet(Guest guest)
        {
            E_mail(guest);
            E_mailGuest(guest);
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                ViewBag.Greeting = guest.Email;
                return View("Thanks", guest);
            }
            else
            {
                return View();
            }
        }

        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "programmeeriminetthk2@gmail.com";
                WebMail.Password = "2.kuursus tarpv20";
                WebMail.From = "programmeeriminetthk2@gmail.com";
                WebMail.Send("programmeeriminetthk2@gmail.com", "Vastus kutsele",guest.Name + " vastas " + ((guest.WillAttend ?? false) ?
                    "tuleb peole " : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch (Exception)
            {
                ViewBag.Message = "Mul on kahjul! Ei saa kirja saada!!!";
            }
        }

        MailMessage message = new MailMessage();
        public void E_mailGuest(Guest guest)
        {
            string komu = guest.Email;
            message.To.Add(new MailAddress(komu));
            message.From = new MailAddress(komu);
            message.Subject = "Ostetud piletid";
            message.Body = guest.Name + "ära unusta tulemast 01.05.2022";
            string email = "programmeeriminetthk2@gmail.com";
            string password = "2.kuursus tarpv20";
            SmtpClient client = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true,
            };
            /*try
            {
                client.Send(message);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {}*/
        }

        GuestContext db = new GuestContext();
        [Authorize] 
        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }
    }
}
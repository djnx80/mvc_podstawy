using myFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myFirstApp.Controllers
{
    public class HomeController : Controller
    {

        private PeoplesEntities _entities = new PeoplesEntities();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.napis = "";     // moja zmienna pomocnicza
   //         Table table = _entities.Table.Find(1);        chciałem wrzucać wartości z tabeli od razu na stronie głównej
            return View();
        }


        // nie działa póki co :/
        public ActionResult Zapisz([Bind(Include = "Id,Imie,Nazwisko,Miasto")] Table table)
        {
            ViewBag.napis = "zapis";
                _entities.Entry(table).State = EntityState.Modified;
                try
                {
                    _entities.SaveChanges();

                } catch {
                }

            return View("Index");
        }

        public ActionResult Odczytaj()
        {
            ViewBag.napis = "odczyt";
            return View(_entities.Table.ToList());
        }



        // GET: PersonalDetails/Edit/5
        public ActionResult Edit()
        {
            // znajdz id = 1 , bo zmieniamy tylko jeden i ten sam rekord, nie dodajemy nowych
            Table table = _entities.Table.Find(1);
            return View(table);
        }


        // metoda wywołana po naciśnięciu przycisku Submit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,Miasto")] Table table)
        {
            // jeżeli wszystko ok to zapisz, 
            if (ModelState.IsValid)
            {
                _entities.Entry(table).State = EntityState.Modified;
                // to powinno wrzucić się w try...catch... zeby wychwycić błąd w razie niepowodzenia zapisu
                _entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }
    }
}
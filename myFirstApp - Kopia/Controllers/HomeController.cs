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
            ViewBag.napis = "";
            return View();
        }

        public ActionResult Zapisz()
        {
            ViewBag.napis = "zapis";
       //     _entities.Table.Add(1,"sfsf", "fdsf", "sfsf");
            return View(_entities.Table.ToList());
        }

        public ActionResult Odczytaj()
        {
            ViewBag.napis = "odczyt";
            return View(_entities.Table.ToList());
        }

        // GET: PersonalDetails/Edit/5
        public ActionResult Edit()
        {
            // znajdz id = 1
            Table table = _entities.Table.Find(1);
            return View(table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Imie,Nazwisko,Miasto")] Table table)
        {
            if (ModelState.IsValid)
            {
                _entities.Entry(table).State = EntityState.Modified;
                _entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }
    }
}
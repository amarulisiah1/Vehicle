using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehicle.Models;
using PagedList;

namespace Vehicle.Controllers
{
    public class MakesController : Controller
    {
        private VehicleContext db = new VehicleContext();

           
        // GET: Makes, i dodati sorting
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page) //ovdje u zagradu dodao: string sortOrder)
        {

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            // ViewBag.ModeliSortParm = sortOrder == "Modeli" ? "modeli_desc" : "Modeli";
            if (searchString != null)    //paging
            { page = 1; }
            else { searchString = currentFilter; }
            ViewBag.CurrentFilter = searchString;
            var makes = from s in db.Makes select s;     //mozda db.Makes

            if (!String.IsNullOrEmpty(searchString))
            { makes = makes.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())); }
            switch (sortOrder)
            {
                case "name_desc":
                    makes = makes.OrderByDescending(s => s.Name);
                    break;

                // case "Modeli":
                //     makes = makes.OrderBy(s => s.Modelis);
                //      break;
                //   case "modeli_desc":
                //      makes = makes.OrderByDescending(s => s.Modelis);
                //     break;
                default:
                    makes = makes.OrderBy(s => s.Name);

                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(makes.ToPagedList(pageNumber, pageSize));
            // return View(makes.ToList()); //dovde je izmijenen
        }




        // GET: Makes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Make make = db.Makes.Find(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
        }

        // GET: Makes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Makes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Abvr")] Make make)
        {
            if (ModelState.IsValid)
            {
                db.Makes.Add(make);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(make);
        }

        // GET: Makes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Make make = db.Makes.Find(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
        }

        // POST: Makes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Abvr")] Make make)
        {
            if (ModelState.IsValid)
            {
                db.Entry(make).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(make);
        }

        // GET: Makes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Make make = db.Makes.Find(id);
            if (make == null)
            {
                return HttpNotFound();
            }
            return View(make);
        }

        // POST: Makes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Make make = db.Makes.Find(id);
            db.Makes.Remove(make);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

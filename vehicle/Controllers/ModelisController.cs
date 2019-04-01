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
    public class ModelisController : Controller
    {
        private VehicleContext db = new VehicleContext();


        // GET: Modelis  add sorting, can search by make or by model

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ModelSortParm = sortOrder == "Model" ? "model_desc" : "Model";
            if (searchString != null)    //paging
            { page = 1; }
            else { searchString = currentFilter; }
            ViewBag.CurrentFilter = searchString;
            var modeli = from m in db.Modelis
                         select m;

            var name = from n in db.Makes select n;   // ovo dodao za ime proizvodjaca 


            if (!String.IsNullOrEmpty(searchString))    // za Filtering
            {
                modeli = modeli.Where(m => m.Name.ToUpper().Contains(searchString.ToUpper())    //filtering by makes  makes je model a Make je proizvodjac
                 || m.Make.Name.ToUpper().Contains(searchString.ToUpper()));    // dodao zbog Filtriranja by Make
            }

            switch (sortOrder)
            {
                case "name_desc":

                    name = name.OrderByDescending(n => n.Name);   //  ????
                    break;
                case "Make":
                    modeli = modeli.OrderBy(m => m.Name);
                    break;
                case "model_desc":
                    modeli = modeli.OrderByDescending(m => m.Name);
                    break;
                default:
                    modeli = modeli.OrderBy(m => m.MakeId);
                    break;
            }
            int pageSize = 8;   // pokazuje broj paginga
            int pageNumber = (page ?? 1);
            return View(modeli.ToPagedList(pageNumber, pageSize));  //ovdje je neka greska u orginalu bilo: return View(modeli.ToPagedList(pageNumber, pageSize));

            //  return View(modeli.ToList());
        }



        // GET: Modelis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modeli modeli = db.Modelis.Find(id);
            if (modeli == null)
            {
                return HttpNotFound();
            }
            return View(modeli);
        }

        // GET: Modelis/Create
        public ActionResult Create()
        {
            ViewBag.MakeId = new SelectList(db.Makes, "Id", "Name");
            return View();
        }

        // POST: Modelis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,MakeId,Abvr")] Modeli modeli)
        {
            if (ModelState.IsValid)
            {
                db.Modelis.Add(modeli);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MakeId = new SelectList(db.Makes, "Id", "Name", modeli.MakeId);
            return View(modeli);
        }

        // GET: Modelis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modeli modeli = db.Modelis.Find(id);
            if (modeli == null)
            {
                return HttpNotFound();
            }
            ViewBag.MakeId = new SelectList(db.Makes, "Id", "Name", modeli.MakeId);
            return View(modeli);
        }

        // POST: Modelis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,MakeId,Abvr")] Modeli modeli)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modeli).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MakeId = new SelectList(db.Makes, "Id", "Name", modeli.MakeId);
            return View(modeli);
        }

        // GET: Modelis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Modeli modeli = db.Modelis.Find(id);
            if (modeli == null)
            {
                return HttpNotFound();
            }
            return View(modeli);
        }

        // POST: Modelis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Modeli modeli = db.Modelis.Find(id);
            db.Modelis.Remove(modeli);
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

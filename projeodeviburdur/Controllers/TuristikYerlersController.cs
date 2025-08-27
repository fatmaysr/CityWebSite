using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projeodeviburdur.Models;


namespace projeodeviburdur.Controllers
{
    public class TuristikYerlersController : Controller
    {
        private burdurvt db = new burdurvt();

        // GET: TuristikYerlers
        public ActionResult Index()
        {
            return View(db.TuristikYerlers.ToList());
        }

        // GET: TuristikYerlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuristikYerler turistikYerler = db.TuristikYerlers.Find(id);
            if (turistikYerler == null)
            {
                return HttpNotFound();
            }
            return View(turistikYerler);
        }

        // GET: TuristikYerlers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TuristikYerlers/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TuristikYerler1")] TuristikYerler turistikYerler)
        {
            if (ModelState.IsValid)
            {
                db.TuristikYerlers.Add(turistikYerler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(turistikYerler);
        }

        // GET: TuristikYerlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuristikYerler turistikYerler = db.TuristikYerlers.Find(id);
            if (turistikYerler == null)
            {
                return HttpNotFound();
            }
            return View(turistikYerler);
        }

        // POST: TuristikYerlers/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TuristikYerler1")] TuristikYerler turistikYerler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turistikYerler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(turistikYerler);
        }

        // GET: TuristikYerlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuristikYerler turistikYerler = db.TuristikYerlers.Find(id);
            if (turistikYerler == null)
            {
                return HttpNotFound();
            }
            return View(turistikYerler);
        }

        // POST: TuristikYerlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TuristikYerler turistikYerler = db.TuristikYerlers.Find(id);
            db.TuristikYerlers.Remove(turistikYerler);
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

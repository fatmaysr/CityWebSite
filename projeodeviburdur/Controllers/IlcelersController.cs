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
    public class IlcelersController : Controller
    {
        private burdurvt db = new burdurvt();

        // GET: Ilcelers
        public ActionResult Index()
        {
            return View(db.Ilcelers.ToList());
        }

        // GET: Ilcelers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilceler ilceler = db.Ilcelers.Find(id);
            if (ilceler == null)
            {
                return HttpNotFound();
            }
            return View(ilceler);
        }

        // GET: Ilcelers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ilcelers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IlceAdi,Nufus")] Ilceler ilceler)
        {
            if (ModelState.IsValid)
            {
                db.Ilcelers.Add(ilceler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ilceler);
        }

        // GET: Ilcelers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilceler ilceler = db.Ilcelers.Find(id);
            if (ilceler == null)
            {
                return HttpNotFound();
            }
            return View(ilceler);
        }

        // POST: Ilcelers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IlceAdi,Nufus")] Ilceler ilceler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ilceler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ilceler);
        }

        // GET: Ilcelers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilceler ilceler = db.Ilcelers.Find(id);
            if (ilceler == null)
            {
                return HttpNotFound();
            }
            return View(ilceler);
        }

        // POST: Ilcelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ilceler ilceler = db.Ilcelers.Find(id);
            db.Ilcelers.Remove(ilceler);
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

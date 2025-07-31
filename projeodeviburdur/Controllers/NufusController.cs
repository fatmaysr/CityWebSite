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
    public class NufusController : Controller
    {
        private burdurvt db = new burdurvt();

        // GET: Nufus
        public ActionResult Index()
        {
            return View(db.Nufus.ToList());
        }

        // GET: Nufus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nufu nufu = db.Nufus.Find(id);
            if (nufu == null)
            {
                return HttpNotFound();
            }
            return View(nufu);
        }

        // GET: Nufus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nufus/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Yil,NufusSayisi")] Nufu nufu)
        {
            if (ModelState.IsValid)
            {
                db.Nufus.Add(nufu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nufu);
        }

        // GET: Nufus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nufu nufu = db.Nufus.Find(id);
            if (nufu == null)
            {
                return HttpNotFound();
            }
            return View(nufu);
        }

        // POST: Nufus/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Yil,NufusSayisi")] Nufu nufu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nufu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nufu);
        }

        // GET: Nufus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nufu nufu = db.Nufus.Find(id);
            if (nufu == null)
            {
                return HttpNotFound();
            }
            return View(nufu);
        }

        // POST: Nufus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nufu nufu = db.Nufus.Find(id);
            db.Nufus.Remove(nufu);
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

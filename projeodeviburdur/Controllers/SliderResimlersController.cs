using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using projeodeviburdur.Models;

namespace projeodeviburdur.Controllers
{
    public class SliderResimlersController : Controller
    {
        private burdurvt db = new burdurvt();

        // AJAX ile resimleri alacak metod
        public JsonResult GetSliderImages()
        {
            // Slider resimlerini veritabanından al
            var sliderResimler = db.SliderResimlers.ToList();

            // JSON formatında döndür
            return Json(sliderResimler, JsonRequestBehavior.AllowGet);
        }

        // GET: SliderResimlers
        public ActionResult Index()
        {
            return View(db.SliderResimlers.ToList());
        }

        // GET: SliderResimlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderResimler sliderResimler = db.SliderResimlers.Find(id);
            if (sliderResimler == null)
            {
                return HttpNotFound();
            }
            return View(sliderResimler);
        }

        // GET: SliderResimlers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SliderResimlers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase resimDosyasi, string altMetin)
        {
            if (resimDosyasi != null && resimDosyasi.ContentLength > 0)
            {
                // Yüklenen dosyanın ismini al
                var dosyaAdi = Path.GetFileName(resimDosyasi.FileName);
                var dosyaYolu = Path.Combine(Server.MapPath("~/Content/Images/"), dosyaAdi);

                // Resmi belirtilen yola kaydet
                resimDosyasi.SaveAs(dosyaYolu);

                // Resim URL'sini oluştur
                var resimUrl = "~/Content/Images/" + dosyaAdi;

                // Yeni slider resmini veritabanına kaydet
                var sliderResim = new SliderResimler
                {
                    ResimUrl = resimUrl,
                    AltMetin = altMetin
                };

                // Veritabanına kaydet
                db.SliderResimlers.Add(sliderResim);
                db.SaveChanges();

                // Başarılı kaydetme sonrası listeye dön
                return RedirectToAction("Index");
            }

            // Eğer resim dosyası yoksa, formu tekrar göster
            return View();
        }

        // GET: SliderResimlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderResimler sliderResimler = db.SliderResimlers.Find(id);
            if (sliderResimler == null)
            {
                return HttpNotFound();
            }
            return View(sliderResimler);
        }

        // POST: SliderResimlers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ResimUrl,AltMetin")] SliderResimler sliderResimler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sliderResimler).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sliderResimler);
        }

        // GET: SliderResimlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SliderResimler sliderResimler = db.SliderResimlers.Find(id);
            if (sliderResimler == null)
            {
                return HttpNotFound();
            }
            return View(sliderResimler);
        }

        // POST: SliderResimlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SliderResimler sliderResimler = db.SliderResimlers.Find(id);
            db.SliderResimlers.Remove(sliderResimler);
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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projeodeviburdur.Models;

namespace projeodeviburdur.Controllers
{
    public class HomeController : Controller
    {
        private burdurvt db = new burdurvt();

        // Ana sayfa açıldığında slider resimlerini çek
        [AllowAnonymous]
        public ActionResult Index()
        {
            var sliderResimler = db.SliderResimlers.ToList(); // SliderResimlers DbSet'inden tüm kayıtları alır
            return View(sliderResimler);
        }

        public ActionResult About()
        {
            // İlçeleri veritabanından çekme
            var ilceler = db.Ilcelers.ToList();
            ViewBag.Ilceler = ilceler;

            // Nüfus verilerini veritabanından çekme
            var nufus = db.Nufus.ToList();
            ViewBag.Nufus = nufus;  // Nüfus verilerini ViewBag üzerinden View'a gönder

            // Turistik Yerler verilerini veritabanından çekme
            var turistikYerler = db.TuristikYerlers.ToList();
            ViewBag.TuristikYerler = turistikYerler;  // Turistik Yerleri ViewBag üzerinden View'a gönder

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        // Kullanıcı Girişi

        public ActionResult Login()
        {
            return View();
        }
        // Kullanıcı Giriş İşlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Kullanicilar kullanici)
        {
            if (ModelState.IsValid)
            {
                var girisYapan = db.Kullanicilars
                    .FirstOrDefault(k => k.KullaniciAdi == kullanici.KullaniciAdi && k.Sifre == kullanici.Sifre);

                if (girisYapan != null)
                {
                    // Oturum açıldığında bilgileri sakla
                    Session["KullaniciAdi"] = girisYapan.KullaniciAdi;
                    Session["Rol"] = girisYapan.Rol;

                    return RedirectToAction("Index"); // Ana sayfaya yönlendir
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                }
            }
            return View(kullanici);
        }

        // Kullanıcı Çıkış Yapma
        public ActionResult Logout()
        {
            Session.Clear(); // Oturumu temizle
            return RedirectToAction("Index"); // Ana sayfaya yönlendir
        }

        // Yönetici Menüsüne Erişim Kontrolü (Örneğin, admin için rol kontrolü)
        public bool KullaniciYoneticiMi()
        {
            return Session["Rol"] != null && Session["Rol"].ToString() == "Yönetici";
        }
    }
}

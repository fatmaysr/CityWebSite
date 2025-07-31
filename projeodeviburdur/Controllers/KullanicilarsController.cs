using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using projeodeviburdur.Models;

namespace projeodeviburdur.Controllers
{
    public class KullanicilarsController : Controller
    {
        private burdurvt db = new burdurvt();

        // GET: Kullanicilars
        [Authorize] // Sadece giriş yapmış kullanıcılar görebilir
        public ActionResult Index()
        {
            return View(db.Kullanicilars.ToList());
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string kullaniciAdi, string sifre)
        {
            // Şifreyi hashle
            string hashedSifre = HashSifre(sifre);

            // Kullanıcı doğrulama
            var kullanici = db.Kullanicilars
                .FirstOrDefault(u => u.KullaniciAdi == kullaniciAdi && u.Sifre == hashedSifre);

            if (kullanici != null)
            {
                // Oturum aç
                Session["KullaniciAdi"] = kullanici.KullaniciAdi;
                Session["Rol"] = kullanici.Rol;

                // Başarılı girişte ana sayfaya yönlendir
                return RedirectToAction("Index", "Home");
            }

            // Hatalı giriş durumu
            ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı.");
            return View();
        }

        // Şifreyi hashlemek için kullanılan fonksiyon
        private string HashSifre(string sifre)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] sifreBytes = Encoding.UTF8.GetBytes(sifre);
                byte[] hashBytes = sha256.ComputeHash(sifreBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Oturumu temizle
            return RedirectToAction("Login", "Kullanicilars");
        }

        // Aşağıdaki CRUD işlemleri yetki gerektirir

        // Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,KullaniciAdi,Sifre,Rol")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                // Şifreyi hashle ve kaydet
                kullanicilar.Sifre = HashSifre(kullanicilar.Sifre);
                db.Kullanicilars.Add(kullanicilar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kullanicilar);
        }

        // Edit
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }

            return View(kullanicilar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,KullaniciAdi,Sifre,Rol")] Kullanicilar kullanicilar)
        {
            if (ModelState.IsValid)
            {
                // Şifre değiştiyse yeniden hashle
                var eskiKullanici = db.Kullanicilars.AsNoTracking().FirstOrDefault(k => k.Id == kullanicilar.Id);
                if (eskiKullanici != null && eskiKullanici.Sifre != kullanicilar.Sifre)
                {
                    kullanicilar.Sifre = HashSifre(kullanicilar.Sifre);
                }

                db.Entry(kullanicilar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kullanicilar);
        }

        // Delete
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
            if (kullanicilar == null)
            {
                return HttpNotFound();
            }

            return View(kullanicilar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Kullanicilar kullanicilar = db.Kullanicilars.Find(id);
            db.Kullanicilars.Remove(kullanicilar);
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

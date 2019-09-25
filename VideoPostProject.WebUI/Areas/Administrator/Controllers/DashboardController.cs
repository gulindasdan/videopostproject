using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoPostProject.WebUI.Models;
using System.Xml;
using System.Xml.Linq;
using VideoPostProject.Service.Option;
using VideoPostProject.Model.Entities;

namespace VideoPostProject.WebUI.Areas.Administrator.Controllers
{
    [AdminAuthentication]
    public class DashboardController : Controller
    {
        UserService us = new UserService();
        VideoService vs = new VideoService();
        CategoryService cs = new CategoryService();
        CountryService ccs = new CountryService();
        // GET: Administrator/Dashboard
        public ActionResult Index()
        {

            return View(Tuple.Create<List<User> , List<Video>>(us.GetActive(), vs.GetActive()));
        }
        public ActionResult GetData()
        {
            int male = us.GetActive().Where(x => x.Gender == Gender.Erkek && x.isAdministrator==false).Count();
            int female = us.GetActive().Where(x => x.Gender == Gender.Kadın && x.isAdministrator == false).Count();
            Ratio obj = new Ratio();
            obj.Male = male;
            obj.Female = female;
            
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int Male { get; set; }
            public int Female { get; set; }
            //public int Other { get; set; }
        }
        public ActionResult GetKategoriData()
        {
            int muzik = vs.GetActive().Where(x => x.Category.CategoryName == "Müzik").Count();
            int haber = vs.GetActive().Where(x => x.Category.CategoryName == "Haber").Count();
            int eglence = vs.GetActive().Where(x => x.Category.CategoryName == "Eğlence").Count();
            int oyun = vs.GetActive().Where(x => x.Category.CategoryName == "Oyun").Count();
            int spor = vs.GetActive().Where(x => x.Category.CategoryName == "Spor").Count();
            int egitim = vs.GetActive().Where(x => x.Category.CategoryName == "Eğitim").Count();
            int blog = vs.GetActive().Where(x => x.Category.CategoryName == "Kişisel Blog").Count();
            int bilim = vs.GetActive().Where(x => x.Category.CategoryName == "Bilim ve Teknoloji").Count();
            int moda = vs.GetActive().Where(x => x.Category.CategoryName == "Güzellik ve Moda").Count();
            int gezi = vs.GetActive().Where(x => x.Category.CategoryName == "Gezi ve Etkinlikler").Count();
            int film = vs.GetActive().Where(x => x.Category.CategoryName == "Film ve Animasyon").Count();
            Ratio2 obj = new Ratio2();
            obj.Muzik = muzik;
            obj.Haber = haber;
            obj.Eglence = eglence;
            obj.Oyun = oyun;
            obj.Spor = spor;
            obj.Egitim = egitim;
            obj.Blog = blog;
            obj.Bilim = bilim;
            obj.Moda = moda;
            obj.Gezi = gezi;
            obj.Film = film;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio2
        {
            public int Muzik { get; set; }
            public int Haber { get; set; }
            public int Eglence { get; set; }
            public int Oyun { get; set; }
            public int Spor { get; set; }
            public int Egitim { get; set; }
            public int Blog { get; set; }
            public int Bilim { get; set; }
            public int Moda { get; set; }
            public int Gezi { get; set; }
            public int Film { get; set; }
        }
        public ActionResult EditProfile(Guid id)
        {
            User user = us.GetByID(id);
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", user.CategoryID);
            ViewBag.CountryID = new SelectList(ccs.GetActive(), "ID", "CountryName", user.CountryID);
            return View(us.GetByID(id));
        }
        [HttpPost]
        public ActionResult EditProfile(User item)
        {
            ViewBag.CategoryID = new SelectList(ccs.GetActive(), "ID", "CategoryName", item.CategoryID);
            ViewBag.CountryID = new SelectList(cs.GetActive(), "ID", "CountryName", item.CountryID);
            //ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), item.Gender);
            if (ModelState.IsValid)
            {
                bool eklemeSonucu = us.Update(item);

                if (eklemeSonucu)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = $"Kullanıcı güncelleme işlemi sırasında bir hata oluştu. Lütfen tekrar deneyin.";
                }
            }
            else
            {
                ViewBag.Message = $"Lütfen girmiş olduğunuz bilgilerin eksiksiz ve doğru formatta olduğundan emin olun.";
            }
            return View();
        }
    }
}
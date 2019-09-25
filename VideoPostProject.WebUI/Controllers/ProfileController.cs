using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoPostProject.Model.Entities;
using VideoPostProject.Service.Option;
using VideoPostProject.WebUI.Models;

namespace VideoPostProject.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        CategoryService cs = new CategoryService();
        SubsribeService fds = new SubsribeService();
        UserService us = new UserService();
        CountryService cts = new CountryService();
        VideoService vs = new VideoService();
        FavoriteService fv = new FavoriteService();
        // GET: Profile

        public ActionResult AboutMe(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            User user = us.GetByID(id);
            ViewBag.Videolar = vs.GetActive().Where(m => m.ID == id).ToList();
            //ViewBag.Fav = fv.GetActive().Where(m => m.UserID == user.ID).Count();
            int sayi= fds.GetActive().Where(m => m.FolowerID == user.ID).Count();
            if (sayi == 0)
            {
                ViewBag.TakipSayi = 0;
            }
            else
            {
                ViewBag.TakipSayi = fds.GetActive().Where(m => m.FolowerID == user.ID).Count();
            }
            if (Session["oturum"] != null)
            {
                User gelen = (User)Session["oturum"];
                ViewBag.TakipEdiyorMu = fds.GetActive().Any(m => m.FolowedID == gelen.ID && m.FolowerID == user.ID);
            }
            return View(us.GetByID(id));
        }
        public ActionResult Videos(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            User user = us.GetByID(id);
            ViewBag.Videolar = vs.GetActive().Where(m => m.User.ID == user.ID).ToList();
            ViewBag.TakipSayi = fds.GetActive().Where(m => m.FolowerID == user.ID).Count();
            if (Session["oturum"] != null)
            {
                User gelen = (User)Session["oturum"];
                ViewBag.TakipEdiyorMu = fds.GetActive().Any(m => m.FolowedID == gelen.ID && m.FolowerID == user.ID);
            }
            return View(us.GetByID(id));
            //return View(Tuple.Create<User, List<Video>>(us.GetByID(id), vs.GetActive().Where(m => m.ID == id).ToList()));
        }

        public ActionResult FavoriteVideos(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            User user = us.GetByID(id);
            //bool sonuc = fv.GetActive().Any(m => m.UserID == user.ID);
            
            //if (sonuc)
            //{
            //    ViewBag.Videolar = fv.GetActive().Where(m => m.UserID == user.ID).ToList();
            //}
            ViewBag.TakipSayi = fds.GetActive().Where(m => m.FolowerID == user.ID).Count();
            if (Session["oturum"] != null)
            {
                User gelen = (User)Session["oturum"];
                ViewBag.TakipEdiyorMu = fds.GetActive().Any(m => m.FolowedID == gelen.ID && m.FolowerID == user.ID);
            }
            return View(Tuple.Create<User,List<Favorite>>(us.GetByID(id),fv.GetActive().Where(m=>m.UserID==user.ID).ToList()));
        }
        public ActionResult Followers(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            User item = us.GetByID(id);
            ViewBag.TakipSayi = fds.GetActive().Where(m => m.FolowerID == item.ID).Count();
            if (Session["oturum"] != null)
            {
                User gelen = (User)Session["oturum"];
                ViewBag.TakipEdiyorMu = fds.GetActive().Any(m => m.FolowedID == gelen.ID && m.FolowerID == item.ID);
            }
            return View(Tuple.Create<User, List<Subsribe>, List<Subsribe>>(us.GetByID(item.ID), fds.GetActive().Where(m => m.FolowedID == item.ID).ToList(), fds.GetActive().Where(m => m.FolowerID == item.ID).ToList()));
        }
        public ActionResult Settings(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            User user = us.GetByID(id);
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", user.CategoryID);
            ViewBag.CountryID = new SelectList(cts.GetActive(), "ID", "CountryName", user.CountryID);
            
            return View(us.GetByID(id));
        }

        [HttpPost]
        public ActionResult Settings(User item, HttpPostedFileBase fluResim)
        {
            ViewBag.Kategoriler = cs.GetActive();
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", item.CategoryID);
            ViewBag.CountryID = new SelectList(cts.GetActive(), "ID", "CountryName", item.CountryID);

            if (ModelState.IsValid)
            {
                if (fluResim != null)
                {
                    bool sonuc2;
                    string fileResult = FxFunction.ImageUpload(fluResim, FolderPath.UserProfil, out sonuc2);
                    
                    if (sonuc2)
                    {
                        item.ImagePath = fileResult;
                    }
                }
                bool sonuc = us.Update(item);
                if (sonuc)
                {
                    return RedirectToAction("AboutMe","Profile",new {id=item.ID });
                }
                else
                {
                    ViewBag.Message = $"Ülke güncelleme işlemi sırasında bir hata oluştu. Lütfen tüm bilgilerinizi kontrol ederek tekrar deneyin.";
                }
            }
            else
            {
                ViewBag.Message = $"Girmiş olduğunuz bilgiler hatalı formatta veya eksiktir. Lütfen girmeye çalıştığınız verileri kontrol edin.";
            }
            return View();

        }
        public ActionResult Follow(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            User user = us.GetByID(id);
            User gelen = (User)Session["oturum"];
            if (Session["oturum"] != null)
            {
                bool sonuc = fds.GetActive().Any(m => m.FolowedID == gelen.ID && m.FolowerID == user.ID);
                if (sonuc)
                {
                    Subsribe silinecek = fds.GetByDefault(m => m.FolowedID == gelen.ID && m.FolowerID == user.ID);
                    fds.Remove(silinecek.ID);
                }
                else
                {
                    Subsribe subsribe = new Subsribe();
                    subsribe.FolowedID = gelen.ID;
                    subsribe.FolowerID = user.ID;
                    subsribe.takipEdiyorMu = true;
                    fds.Add(subsribe);

                    ViewBag.TakipEdiyorMu = fds.GetActive().Any(m => m.FolowedID == gelen.ID && m.FolowerID == user.ID);
                }
                List<Subsribe> liste = fds.GetActive().Where(m => m.FolowedID == gelen.ID).ToList();
                return Json(liste, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }
    }
}
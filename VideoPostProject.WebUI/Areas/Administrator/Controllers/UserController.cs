using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoPostProject.Model.Entities;
using VideoPostProject.Service.Option;
using VideoPostProject.WebUI.Models;

namespace VideoPostProject.WebUI.Areas.Administrator.Controllers
{
    [AdminAuthentication]
    public class UserController : Controller
    {
        UserService us = new UserService();
        CategoryService ccs = new CategoryService();
        CountryService cs = new CountryService();
        // GET: Administrator/User
        public ActionResult Index()
        {
            return View(us.GetAll(us => us.Name));
        }
        public ActionResult Insert()
        {
            ViewBag.CategoryID = new SelectList(ccs.GetActive(), "ID", "CategoryName");
            ViewBag.CountryID = new SelectList(cs.GetActive(), "ID", "CountryName");
            //ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)));

            return View();
        }
        [HttpPost]
        public ActionResult Insert(User item, HttpPostedFileBase fluResim, HttpPostedFileBase fluResim2)
        {
            ViewBag.CategoryID = new SelectList(ccs.GetActive(), "ID", "CategoryName", item.CategoryID);
            ViewBag.CountryID = new SelectList(cs.GetActive(), "ID", "CountryName", item.CountryID);
            //ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), item.Gender);
            if (ModelState.IsValid)
            {
                if (fluResim != null || fluResim2 != null)
                {
                    bool sonuc, sonuc2;
                    string fileResult = FxFunction.ImageUpload(fluResim, FolderPath.UserProfil, out sonuc);
                    string fileResult2 = FxFunction.ImageUpload(fluResim2, FolderPath.UserCoverImage, out sonuc2);
                    if (sonuc || sonuc2)
                    {
                        item.ImagePath = fileResult;
                        item.CoverImagePath = fileResult2;
                    }
                }
                bool eklemeSonucu = us.Add(item);

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
        public ActionResult Update(Guid id)
        {
            User user = us.GetByID(id);
            ViewBag.CategoryID = new SelectList(ccs.GetActive(), "ID", "CategoryName", user.CategoryID);
            ViewBag.CountryID = new SelectList(cs.GetActive(), "ID", "CountryName", user.CountryID);
            //ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), user.Gender);
            return View(us.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(User item, HttpPostedFileBase fluResim, HttpPostedFileBase fluResim2)
        {
            ViewBag.CategoryID = new SelectList(ccs.GetActive(), "ID", "CategoryName", item.CategoryID);
            ViewBag.CountryID = new SelectList(cs.GetActive(), "ID", "CountryName", item.CountryID);
            //ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), item.Gender);
            if (ModelState.IsValid)
            {
                if (fluResim != null || fluResim2 != null)
                {
                    bool sonuc, sonuc2;
                    string fileResult = FxFunction.ImageUpload(fluResim, FolderPath.UserProfil, out sonuc);
                    string fileResult2 = FxFunction.ImageUpload(fluResim2, FolderPath.UserCoverImage, out sonuc2);
                    if (sonuc || sonuc2)
                    {
                        item.ImagePath = fileResult;
                        item.CoverImagePath = fileResult2;
                    }
                }
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

        public ActionResult Delete(Guid id)
        {
            us.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
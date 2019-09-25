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
    public class CategoryController : Controller
    {
        CategoryService cs = new CategoryService();
        // GET: Administrator/Category
        public ActionResult Index()
        {
            return View(cs.GetAll(cs => cs.CategoryName));
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Category item)
        {
            if (ModelState.IsValid)
            {
                bool sonuc = cs.Add(item);
                if (sonuc)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = $"Kategori ekleme işlemi sırasında bir hata oluştu. Lütfen tüm bilgilerinizi kontrol ederek tekrar deneyin.";
                }
            }
            else
            {
                ViewBag.Message = $"Girmiş olduğunuz bilgiler hatalı formatta veya eksiktir. Lütfen girmeye çalıştığınız verileri kontrol edin.";
            }
            return View();
        }
        public ActionResult Update(Guid id)
        {
            return View(cs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Category item)
        {
            if (ModelState.IsValid)
            {
                bool sonuc = cs.Update(item);
                if (sonuc)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = $"Kategori güncelleme işlemi sırasında bir hata oluştu. Lütfen tüm bilgilerinizi kontrol ederek tekrar deneyin.";
                }
            }
            else
            {
                ViewBag.Message = $"Girmiş olduğunuz bilgiler hatalı formatta veya eksiktir. Lütfen girmeye çalıştığınız verileri kontrol edin.";
            }

            return View();
        }
        public ActionResult Delete(Guid id)
        {
            cs.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
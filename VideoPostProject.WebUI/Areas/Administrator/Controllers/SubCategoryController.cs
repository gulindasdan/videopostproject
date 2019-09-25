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
    public class SubCategoryController : Controller
    {
        SubCategoryService scs = new SubCategoryService();
        CategoryService cs = new CategoryService();
        // GET: Administrator/SubCategory
        public ActionResult Index()
        {
            return View(scs.GetAll(scs=>scs.SubCategoryName));
        }
        public ActionResult Insert()
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(),"ID","CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Insert(SubCategory item)
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", item.CategoryID);
            if (ModelState.IsValid)
            {
                bool sonuc = scs.Add(item);
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
            SubCategory subCategory = scs.GetByID(id);
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", subCategory.CategoryID);
            return View(scs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(SubCategory item)
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", item.CategoryID);
            if (ModelState.IsValid)
            {
                bool sonuc = scs.Update(item);
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
            scs.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
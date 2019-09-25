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
    public class CountryController : Controller
    {
        CountryService cs = new CountryService();
        // GET: Administrator/Country
        public ActionResult Index()
        {
            return View(cs.GetAll(cs=>cs.CountryName));
        }
        public ActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Country item)
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
                    ViewBag.Message = $"Ülke ekleme işlemi sırasında bir hata oluştu. Lütfen tüm bilgilerinizi kontrol ederek tekrar deneyin.";
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
        public ActionResult Update(Country item)
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
                    ViewBag.Message = $"Ülke güncelleme işlemi sırasında bir hata oluştu. Lütfen tüm bilgilerinizi kontrol ederek tekrar deneyin.";
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
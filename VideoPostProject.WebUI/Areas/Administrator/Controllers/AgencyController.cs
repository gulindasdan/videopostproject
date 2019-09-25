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
    public class AgencyController : Controller
    {
        AgencyService ags = new AgencyService(); 
        // GET: Administrator/Agency
        public ActionResult Index()
        {
            return View(ags.GetAll(ags=>ags.AgencyName));
        }
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Agency item)
        {
            if (ModelState.IsValid)
            {
                bool sonuc = ags.Add(item);
                if (sonuc)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = $"Ajans ekleme işlemi sırasında bir hata oluştu. Lütfen tüm bilgilerinizi kontrol ederek tekrar deneyin.";
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
            return View(ags.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Agency item)
        {
            if (ModelState.IsValid)
            {
                bool sonuc = ags.Update(item);
                if (sonuc)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = $"Ajans güncelleme işlemi sırasında bir hata oluştu. Lütfen tüm bilgilerinizi kontrol ederek tekrar deneyin.";
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
            ags.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
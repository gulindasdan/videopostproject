using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoPostProject.Model.Entities;
using VideoPostProject.Service.Option;

namespace VideoPostProject.WebUI.Controllers
{
    public class LoginController : Controller
    {
        UserService us = new UserService();
        CountryService cs = new CountryService();
        CategoryService ccs = new CategoryService();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User item)
        {
            bool bilgilerDogru = us.CheckLogin(item.EmailAddress, item.Password);
            if (bilgilerDogru)
            {
                User girisYapan = us.GetByDefault(x => x.EmailAddress == item.EmailAddress && x.Password == item.Password);
                ViewBag.KullaniciAdi = girisYapan.Name + " " + girisYapan.Surname;
                Session["oturum"] = girisYapan;
                if (girisYapan.isAdministrator)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Administrator" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Message = $"***Girmiş olduğunuz bilgiler yanlış veya hatalı";
                return View();
            }
        }
        public ActionResult Register()
        {
            ViewBag.CountryID = new SelectList(cs.GetActive(), "ID", "CountryName");
            ViewBag.CategoryID = new SelectList(ccs.GetActive(), "ID", "CategoryName");
            //ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), Gender.Kadın);
            return View();
        }
        [HttpPost]
        public ActionResult Register(User item, string chkTerms)
        {
            ViewBag.CountryID = new SelectList(cs.GetActive(), "ID", "CountryName", item.CountryID);
            ViewBag.CategoryID = new SelectList(ccs.GetActive(), "ID", "CategoryName", item.CategoryID);
            //ViewBag.Gender = new SelectList(Enum.GetValues(typeof(Gender)), item.Gender);
            if (ModelState.IsValid)
            {
                bool sonuc = us.GetActive().Any(m => m.Username == item.Username);
                bool sonuc2= us.GetActive().Any(m => m.EmailAddress == item.EmailAddress);
                if (sonuc || sonuc2)
                {
                    if (sonuc)
                    {
                        ViewBag.MessageKullanici = "Aynı kullanıcı adına sahip kullanıcı mevcut. Lütfen farklı bir kullanıcı adı deneyiniz!";
                    }
                    else
                    {
                        ViewBag.MessageEmail = "Aynı mail adresine sahip kullanıcı mevcut. Lütfen farklı bir mail adresi deneyiniz!";
                    }
                    return View();
                }

                if (chkTerms == "checked")
                {
                    us.Add(item);
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    ViewBag.Message = "***Ön Bilgilendirme Şartları ve Koşullarını kabul etmeniz gerekiyor.";
                    return View();
                }

            }
            else
            {
                ViewBag.Message = $"***Girmiş olduğunuz bilgiler yanlış veya hatalı";
                return View();
            }

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}
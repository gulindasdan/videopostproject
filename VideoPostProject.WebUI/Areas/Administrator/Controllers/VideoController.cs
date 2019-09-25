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
    public class VideoController : Controller
    {
        VideoService vs = new VideoService();
        CategoryService cs = new CategoryService();
        UserService us = new UserService();
        
        // GET: Administrator/Video
        public ActionResult Index()
        {
            User gelen = (User)Session["oturum"];
            ViewBag.Name = gelen.Name + " " + gelen.Surname;
            return View(vs.GetAll(vs=>vs.Title));
        }
        public ActionResult Insert()
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName");
            //ViewBag.UserID = new SelectList(us.GetActive(), "ID", "Name");
           
            return View();
        }
        [HttpPost]
        public ActionResult Insert(Video item, HttpPostedFileBase fluVideo)
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", item.CategoryID);
            //ViewBag.UserID = new SelectList(us.GetActive(), "ID", "Name", item.UserID);
            //string[] characters = {","," ,",", "," , " };
            //string text = item.VideoTags.ToString();
            //Tag tag = text.Split(characters);
            if (ModelState.IsValid)
            {
                bool sonuc;
                string fileResult = FxFunction.VideoUpload(fluVideo, FolderPath.Video, out sonuc);
                if (sonuc)
                {
                    item.VideoPath = fileResult;
                    User gelen = (User)Session["oturum"];
                    item.UserID = gelen.ID;
                    bool eklemeSonucu = vs.Add(item);
                    if (eklemeSonucu)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = $"Ekleme işlemi sırasında bir hata oluştu.";
                    }
                }
                else
                {
                    ViewBag.Message = fileResult;
                }
            }
            return View();
        }
        public ActionResult Update(Guid id)
        {
            Video video = vs.GetByID(id);
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", video.CategoryID);
            //ViewBag.UserID = new SelectList(us.GetActive(), "ID", "Name", video.UserID);
            return View(vs.GetByID(id));
        }
        [HttpPost]
        public ActionResult Update(Video item, HttpPostedFileBase fluVideo)
        {
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", item.CategoryID);
            //ViewBag.UserID = new SelectList(us.GetActive(), "ID", "Name", item.UserID);
            if (ModelState.IsValid)
            {
                bool sonuc;
                string fileResult = FxFunction.VideoUpload(fluVideo, FolderPath.Video, out sonuc);
                if (sonuc)
                {
                    item.VideoPath = fileResult;
                    bool eklemeSonucu = vs.Update(item);
                    if (eklemeSonucu)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = $"Güncelleme işlemi sırasında bir hata oluştu.";
                    }
                }
                else
                {
                    ViewBag.Message = fileResult;
                }
            }
            return View();
        }
        public ActionResult Delete(Guid id)
        {
            vs.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
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
    public class VideoController : Controller
    {
        CategoryService cs = new CategoryService();
        VideoService vs = new VideoService();
        LikeVideoService lvs = new LikeVideoService();
        DislikeVideoService dvs = new DislikeVideoService();
        CommentService cm = new CommentService();
        FavoriteService fs = new FavoriteService();
        DisplayingService ds = new DisplayingService();
        SubsribeService fds = new SubsribeService();
        // GET: Video
        public ActionResult Upload()
        {
            ViewBag.Kategoriler = cs.GetActive();
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult Upload(Video item, HttpPostedFileBase fluVideo)
        {
            ViewBag.Kategoriler = cs.GetActive();
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName", item.CategoryID);
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
                        return RedirectToAction("Index", "Home");
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
            ViewBag.Kategoriler = cs.GetActive();
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName");
            return View(vs.GetByID(id));
        }

        [HttpPost]
        public ActionResult Update(Video item, HttpPostedFileBase fluVideo)
        {
            ViewBag.Kategoriler = cs.GetActive();
            ViewBag.CategoryID = new SelectList(cs.GetActive(), "ID", "CategoryName");
            if (ModelState.IsValid)
            {
                if (fluVideo != null)
                {
                    bool sonuc;
                    string fileResult = FxFunction.ImageUpload(fluVideo, FolderPath.UserProfil, out sonuc);

                    if (sonuc)
                    {
                        item.VideoPath = fileResult;
                    }
                }

                bool eklemeSonucu = vs.Update(item);
                if (eklemeSonucu)
                {
                    return RedirectToAction("Videos","Profile",new { id = item.UserID });
                }
                else
                {
                    ViewBag.Message = $"Güncelleme işlemi sırasında bir hata oluştu.";
                }

            }
            else
            {
                ViewBag.Message = $"Lütfen girmiş olduğunuz bilgilerin eksiksiz ve doğru formatta olduğundan emin olun.";
            }
            return View();
        }

        public ActionResult WatchVideo(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            Video vid = vs.GetByID(id);
            User gelen = (User)Session["oturum"];
            ViewBag.Comment = cm.GetActive().Where(m => m.Video.ID == vid.ID);
            vid.DisplayingNumber = ds.GetActive().Where(m => m.VideoID == vid.ID).Count();
            ViewBag.TakipSayi = fds.GetActive().Where(m => m.FolowerID == vid.User.ID).Count();

            if (Session["oturum"] != null)
            {
                ViewBag.Favori = fs.GetActive().Any(m => m.Video.ID == vid.ID && m.UserID == gelen.ID);
                
                bool varMi = ds.GetActive().Any(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                if (varMi)
                {
                    vid.DisplayingNumber = ds.GetActive().Where(m => m.VideoID == vid.ID).Count();
                }
                else
                {
                    Displaying disp = new Displaying();
                    disp.UserID = gelen.ID;
                    disp.VideoID = vid.ID;
                    bool sonuc = ds.Add(disp);
                    if (sonuc)
                    {
                        vid.DisplayingNumber = ds.GetActive().Where(m => m.VideoID == vid.ID).Count();
                    }
                }
            }
            return View(Tuple.Create<Video, List<Comment>, Comment, List<Subsribe>>(vs.GetByID(id), cm.GetActive().Where(m => m.VideoID == id).ToList() , new Comment(), fds.GetActive()));
        }
        public ActionResult DeleteVideo(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            User gelen = (User)Session["oturum"];
            vs.Remove(id);
            return RedirectToAction("Videos", "Profile", new { id = gelen.ID });
        }


        public ActionResult Like(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            Video vid = vs.GetByID(id);
            int likeNumber = lvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
            int dislikeNumber = dvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
            User gelen = (User)Session["oturum"];
            if (Session["oturum"] != null)
            {
                bool varMi = lvs.GetActive().Any(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                bool dislikeVarMi = dvs.GetActive().Any(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                if (varMi)
                {
                    LikeVideo silinecek = lvs.GetByDefault(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                    lvs.Remove(silinecek.ID);
                    likeNumber = lvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
                    return Json(likeNumber, JsonRequestBehavior.AllowGet);
                }
                if (dislikeVarMi)
                {
                    DislikeVideo silinecek = dvs.GetByDefault(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                    dvs.Remove(silinecek.ID);
                    dislikeNumber = dvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
                }
                else
                {

                    LikeVideo like = new LikeVideo();
                    like.UserID = gelen.ID;
                    like.VideoID = vid.ID;
                    like.Liked = true;
                    bool sonuc = lvs.Add(like);
                    if (sonuc)
                    {
                        likeNumber = lvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
                        return Json(likeNumber, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Dislike(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            Video vid = vs.GetByID(id);
            User gelen = (User)Session["oturum"];
            int dislikeNumber = dvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
            int likeNumber = lvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
            if (Session["oturum"] != null)
            {
                bool varMi = dvs.GetActive().Any(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                bool likeVarMi = lvs.GetActive().Any(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                if (likeVarMi)
                {
                    LikeVideo silinecek = lvs.GetByDefault(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                    lvs.Remove(silinecek.ID);
                    likeNumber = lvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
                }
                if (varMi)
                {
                    DislikeVideo silinecek = dvs.GetByDefault(m => m.UserID == gelen.ID && m.VideoID == vid.ID);
                    dvs.Remove(silinecek.ID);
                    dislikeNumber = dvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
                    return Json(dislikeNumber, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    DislikeVideo dislike = new DislikeVideo();
                    dislike.UserID = gelen.ID;
                    dislike.VideoID = vid.ID;
                    dislike.Liked = true;
                    bool sonuc = dvs.Add(dislike);
                    if (sonuc)
                    {
                        dislikeNumber = dvs.GetActive().Where(m => m.VideoID == vid.ID).Count();
                        return Json(dislikeNumber, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Favorite(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            Video vid = vs.GetByID(id);
            User gelen = (User)Session["oturum"];
            bool sonuc = fs.GetActive().Any(m=>m.VideoID==vid.ID && m.UserID==gelen.ID);
            if (sonuc)
            {
                Favorite silinecek = fs.GetByDefault(m =>m.Status != Core.Entity.Enum.Status.Deleted && m.UserID == gelen.ID && m.VideoID == vid.ID);
                fs.Remove(silinecek.ID);
                //ViewBag.Favori = fs.GetByDefault(m => m.VideoID == vid.ID && m.UserID == gelen.ID);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (Session["oturum"] != null)
                {
                    Favorite favori = new Favorite();
                    favori.UserID = gelen.ID;
                    favori.VideoID = vid.ID;
                    fs.Add(favori);
                    ViewBag.Favori = fs.GetByDefault(m => m.VideoID == vid.ID && m.UserID==gelen.ID);
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult Comments(string yorum, Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            User gelen = (User)Session["oturum"];
            Video vid = vs.GetByID(id);
            if (yorum == null)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            Comment comment = new Comment();
            comment.UserID = gelen.ID;
            comment.VideoID = vid.ID;
            comment.CommentText = yorum;
            cm.Add(comment);
            List<Comment> liste = cm.GetActive().Where(m => m.VideoID == vid.ID).ToList();
            return Json(liste, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(Guid id)
        {
            User gelen = (User)Session["oturum"];
            fs.Remove(id);
            return RedirectToAction("FavoriteVideos", "Profile", new { id = gelen.ID });
        }
    }
}
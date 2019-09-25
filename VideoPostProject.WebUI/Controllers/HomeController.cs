using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoPostProject.Model.Entities;
using VideoPostProject.Service.Option;

namespace VideoPostProject.WebUI.Controllers
{
    public class HomeController : Controller
    {
        CategoryService cs = new CategoryService();
        VideoService vs = new VideoService();
        UserService us = new UserService();
        DisplayingService ds = new DisplayingService();
        SubsribeService fds = new SubsribeService();

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Kategoriler = cs.GetActive();
            return View(vs.GetActive());
        }

        public ActionResult CategoryPage(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            Category item = cs.GetByID(id);
            ViewBag.KategoriAdi = item.CategoryName;
            ViewBag.KategoriID = item.ID;

            //Tuple.Create<List<Category>, List<Product>>(cs.GetActive(), ps.GetActive())
            return View(Tuple.Create<List<Video>,List<User>, List<Subsribe>>(vs.GetActive().Where(m => m.CategoryID == id).ToList(), us.GetActive().Where(m => m.CategoryID == id).ToList(), fds.GetActive()));
        }
        public ActionResult CategoryPageVideos(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            Category item = cs.GetByID(id);
            ViewBag.KategoriAdi = item.CategoryName;
            ViewBag.KategoriID = item.ID;
            
            return View(vs.GetActive().Where(m => m.CategoryID == id ).ToList());
        }
        public ActionResult CategoryPageChannels(Guid id)
        {
            ViewBag.Kategoriler = cs.GetActive();
            Category item = cs.GetByID(id);
            ViewBag.KategoriAdi = item.CategoryName;
            ViewBag.KategoriID = item.ID;
           
            return View(Tuple.Create<List<User>, List<Subsribe>>(us.GetActive().Where(m => m.CategoryID == id).ToList(), fds.GetActive()));
        }
       
    }
}
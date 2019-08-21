using Rejime.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rejime.Controllers
{
    public class NewsController : Controller
    {
        News NewsTable = new News();
        EF db = new EF();

        public ActionResult Index(int pageID = 1)
        {
            int skip = (pageID - 1) * 5;

            int Count = db.News.Count();
            ViewBag.PageID = pageID;
            ViewBag.PageCount = Count / 5;

            var list = db.News.OrderBy(id => id.ID).Skip(skip).Take(5).ToList();
            return View(list);


        }

        public ActionResult Details(int id)
        {
            return View(model: NewsTable.Read(id));
        }

    }
}
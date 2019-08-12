using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rejime.Models;



namespace Rejime.Controllers
{
    public class HomeController : Controller
    {

        #region khodadadi
        // GET: /Home/
        [HttpGet]
        public ActionResult Index()
        {
            return View(new User());
        }
        public ActionResult LoadMenu()
        {
            try
            {
                var result = DALS.ObjMenu.Select();
                return View("Menu", result);
            }
            catch (Exception ex)
            {
                var error = ex.Message.ToString();
                return Content("Error");
            }
        }
        #endregion


    }
}
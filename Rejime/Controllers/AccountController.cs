using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Rejime.Models;

namespace Rejime.Controllers
{
    public class AccountController : Controller
    {
        #region khodadadi
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //Send Authentication Linke 
        public ActionResult SendAuthenticationLink(User obj)
        {
            if (ModelState.IsValid)
            {
               ViewBag.message = obj.SendAuthenticationLink();

            }
            return Redirect("~/Home/Index");
            //  return View("Menu",DALS.ObjMenu.Select());
        }
        public ActionResult Confirm(string reg)
        {
            string s = reg;

            return View();
        }
        #endregion

    }
}
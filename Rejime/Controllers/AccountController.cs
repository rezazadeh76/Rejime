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
        public ContentResult SendAuthenticationLink(User obj)
        {
            if (ModelState.IsValid)
               return Content(obj.SendAuthenticationLink());

            return Content("");
        }
        public ActionResult Confirm(string reg)
        {
            string s = reg;

            return View();
        }
        #endregion

    }
}
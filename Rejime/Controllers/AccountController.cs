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
        //Send Authentication Linke 
        public ActionResult SendAuthenticationLink(FormCollection obj)
        {
            DALS.ObjUser.FirstName = obj["txtName"];
            DALS.ObjUser.LastName = obj["txtFamily"];
            DALS.ObjUser.Email = obj["txtMail"];
            ViewBag.message = DALS.ObjUser.SendAuthenticationLink();
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
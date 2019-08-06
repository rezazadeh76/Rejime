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
        [HttpPost]
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
            if (DALS.ObjUser.CheckCodeConfirm(reg))
            {
                return View(model:"<div class='alert alert-success col-6'> ثبت نام شما <strong> با موفقیت </strong> انجام شد</div>");
            }
            return View(model:"<div class='alert alert-danger col-6'>شما به این صفحه دسترسی ندارید</div>");
        }
        #endregion

    }
}
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
            {
                obj.ID_gender = 2;
                return Content(obj.SendAuthenticationLink());
            }

            return Content("");
        }
        public ActionResult Confirm(string reg)
        {
            if (DALS.ObjUser.CheckCodeConfirm(reg))
            {
                return View(model: "<div class='alert alert-success col-6'> ثبت نام شما<strong> با موفقیت </strong>  انجام شد &nbsp;<span class='fa fa-check' style='font-size:28px'></span></div>");
            }
            return View(model: "<div class='alert alert-danger col-6'>شما به این صفحه دسترسی ندارید &nbsp;<span class='fa fa-warning' style='font-size:28px'></span></div>");
        }
        #endregion

    }
}
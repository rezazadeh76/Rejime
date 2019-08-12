﻿using System;
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
                return Content(obj.SendAuthenticationLink());
        }
        public ActionResult Confirm(string reg)
        {
            ViewBag.alert = "";
            if (DALS.ObjUser.CheckCodeConfirm(reg))
            {
                ViewBag.alert = "<div class='alert alert-success offset-lg-3 col-lg-6 offset-lg-3 col-lg-6 offset-sm-2 col-sm-8 offset-1 col-10 ' style='font - size:1.1rem'> ثبت نام شما<strong> با موفقیت </strong>  انجام شد &nbsp;<span class='fa fa-check' style='font-size:28px'></span></div>";
                return View(new User());
            }
            return View(new User());
        }
        #endregion

    }
}
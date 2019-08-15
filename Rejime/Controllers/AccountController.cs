using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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
                ViewBag.alert = "<div class='alert alert-success offset-lg-3 col-lg-6 offset-lg-3 col-lg-6 offset-sm-2 col-sm-8 offset-1 col-10 mt-3' style='font - size:1.1rem'> ثبت نام شما<strong> با موفقیت </strong>  انجام شد &nbsp;<span class='fa fa-check' style='font-size:28px'></span></div>";
                return View( new EF().User.Where(x => x.CodeConfirm == reg).Single());
            }
            return View();
        }
        [HttpPost]
        public JsonResult Confirm(User obj)
        {
            
                try
                {
                    var prepareForJson = new
                    {
                        id = obj.id,
                        FirstName = obj.FirstName,
                        LastName = obj.LastName,
                        UserName = obj.UserName,
                        Passwords = obj.Passwords,
                        ConfirmPassword = obj.ConfirmPassword,
                        ID_gender = obj.ID_gender,
                        Email = obj.Email,
                        Moblie = obj.Moblie,
                        ImageName = obj.ImageName,
                        ImageContent = obj.ImageContent,
                        Image = obj.Image,
                        Date = obj.Date,
                        Time = obj.Time,
                        Active = obj.Active,
                        CodeConfirm = obj.CodeConfirm,
                    };

                //return Json(obj);
                string res = "";
                res = Rejime.Models.DALS.ResulToJson(new
                {
                    data = obj,
                    error = false,
                    message = "",
                });
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
                {

                    throw;
                }
                
  

        }
        #endregion

    }
}
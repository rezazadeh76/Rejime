using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.Entity;
using Rejime.Models;
//for send mail
using System.Net;
using System.Net.Mail;
using System.Web.Hosting;
using System.ComponentModel;

namespace Rejime.Models
{
    public class User:DALS
    {
        EF db = new EF();


        public int id { get; set; }
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string Passwords { get; set; }
        [ForeignKey("genderTable")]
        public int ID_gender { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(11)]
        public string Moblie { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        [StringLength(20)]
        public string ImageContent { get; set; }
        public byte[] Image { get; set; }
        [StringLength(10)]
        public string Date { get; set; }
        [StringLength(10)]
        public string Expire { get; set; }
        [DefaultValue("false")]
        public bool Active { get; set; }
        public string CodeConfirm { get; set; }
        public virtual Gender genderTable { get; set; }

        EF entity = new EF();
   
        public string SendAuthenticationLink()
        {
            string token = NewToken();
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/Account/EmailTemplate/")+ "Text"+".cshtml");
            //var regInfo = db.User.Where(x => x.id == regID).FirstOrDefault();
            var url = "http://localhost:7872/" + "Account/Confirm?reg=" + token;
            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.Replace("Family", this.LastName+ ' '+this.FirstName );
            body = body.ToString();

            var client = new SmtpClient("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential("salamatyarr@gmail.com", "5713Love262*2");
            client.EnableSsl = true;
            MailMessage message = new MailMessage("salamatyarr@gmail.com", this.Email, "تکمیل فرایند ثبت نام", body);
            message.IsBodyHtml = true;
            try
            {
                client.Send(message);
                this.CodeConfirm = token;
                this.Create(this);
                return "لینک فعال سازی به ایمیل شما ارسال شده است، لطفا ایمیل خود را بررسی نمایید";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return "در هنگام ارسال لینک فعال سازی مشکلی به وجود آمده است";
            }
        }
        //public bool Authentication()
        //{
        //    return entity.User.Any(item => item.UserName == UserName && item.Passwords == Passwords);
        //}

        public List<User> Read()
        {
            return entity.User.ToList();
        }
        public User Read(int? id)
        {
            return entity.User.Find(id);
        }
        public string Create(User obj)
        {
            entity.User.Add(obj);

            try
            {
                entity.SaveChanges();
                return "اطلاعات با موفقیت ذخیره شد";
            }
            catch (Exception ex)
            {
                return "در ذخیره سازی اطلاعات مشکلی رخ داده است";
            }
        }
        //public string Update(User obj)
        //{
        //    entity.User.Attach(obj);
        //    entity.Entry(obj).State = EntityState.Modified;
        //    try
        //    {
        //        entity.SaveChanges();
        //        return "اطلاعات با موفقیت ویرایش شد";
        //    }
        //    catch (Exception)
        //    {

        //        return "در ویرایش اطلاعات مشکلی رخ داده است";
        //    }
        //}
        //public string Update(int ID, string FirstName, string LastName, string UserName, string Passwords, string Email)
        //{
        //    var user = entity.User.Find(ID);
        //    user.FirstName = FirstName;
        //    user.LastName = LastName;
        //    user.Email = Email;
        //    user.UserName =UserName ;
        //    user.Passwords = Passwords;
        //    try
        //    {
        //        entity.SaveChanges();
        //        return "اطلاعات با موفقیت ویرایش شد";
        //    }
        //    catch (Exception)
        //    {

        //        return "در ویرایش اطلاعات مشکلی رخ داده است";
        //    }
        //}
        //public string Delete(int ID)
        //{
        //    if (entity.User.Find(ID)!=null)
        //    {
        //        entity.User.Remove(entity.User.Find(ID));

        //        try
        //        {
        //            entity.SaveChanges();
        //            return "اطلاعات با حذف ذخیره شد";
        //        }
        //        catch (Exception)
        //        {

        //            return "در حذف اطلاعات مشکلی رخ داده است";
        //        }
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
    }


}
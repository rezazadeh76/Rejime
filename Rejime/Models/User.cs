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
//for Timer
using System.Timers;
using System.Data.Entity.Validation;
using System.Web.Security;

namespace Rejime.Models
{
    public class User:DALS
    {
        public User()
        {
            ID_gender = 2;
            UserName = "admin";
            Passwords = "@admin";
            ConfirmPassword = "@admin";
        }
        #region khodadadi
        public int id { get; set; }

        [RegularExpression(@"^([آ-ی ءa-zA-Z]+\S?)$", ErrorMessage = "مقدار وارد شده صحیح نمی باشد")]
        [StringLength(100, ErrorMessage = "طول بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا نام را وارد نمایید")]
        [Display(Name = "نام*")]
        public string FirstName { get; set; }

        [RegularExpression(@"^([آ-ی ءa-zA-Z]+\S?)$", ErrorMessage = "مقدار وارد شده صحیح نمی باشد")]
        [StringLength(100, ErrorMessage = "طول بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمایید")]
        [Display(Name = "نام خانوادگی*")]
        public string LastName { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_\W]\S+$", ErrorMessage = "مقدار وارد شده صحیح نمی باشد")]
        [Required(ErrorMessage = "لطفا نام کاربری را وارد نمایید")]
        [StringLength(100,ErrorMessage ="طول بیش از حد مجاز است")]
        [Display(Name ="نام کاربری*")]
        public string UserName { get; set; }

        [StringLength(20, ErrorMessage = "طول بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا کلمه عبور را وارد نمایید")]
        [RegularExpression("^.{6,}$",ErrorMessage ="حداقل طول کلمه عبور 6 کراکتر است")]
        [Display(Name = "کلمه عبور*")]
        public string Passwords { get; set; }


        [StringLength(20, ErrorMessage = "طول بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا تایید کلمه عبور را وارد نمایید")]
        [System.Web.Mvc.Compare("Passwords", ErrorMessage = " کلمه عبور و تایید کلمه عبور مطابقت ندارند")]
        [Display(Name = "تایید کلمه عبور*")]
        public string ConfirmPassword { get; set; }

        [ForeignKey("genderTable")]
        public int ID_gender { get; set; }

        [StringLength(100, ErrorMessage = "طول بیش از حد مجاز است")]
        [Required(ErrorMessage = "لطفا ایمیل را وارد نمایید")]
        [Display(Name = "ایمیل*")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
         @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "مقدار وارد شده صحیح نمی باشد")]
        public string Email { get; set; }


        [StringLength(11, ErrorMessage = "طول بیش از حد مجاز است")]
        [Display(Name = "موبایل")]
        public string Moblie { get; set; }
        [StringLength(50)]
        public string ImageName { get; set; }
        [StringLength(20)]
        public string ImageContent { get; set; }
        public byte[] Image { get; set; }
        [StringLength(10)]
        public string Date { get; set; }
        [StringLength(10)]
        public string Time { get; set; }
        [DefaultValue("false")]
        public bool Active { get; set; }
        [StringLength(32)]
        public string CodeConfirm { get; set; }
        public virtual Gender genderTable { get; set; }

        static  EF entity = new EF();
   
        public string SendAuthenticationLink()
        {
            string token = NewToken();
            string body = System.IO.File.ReadAllText(HostingEnvironment.MapPath("~/Views/Account/EmailTemplate/")+ "Text"+".cshtml");
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

                //گرفتن تاریخ و ساعت جاری 
                var DateTimeCurrent = entity.Database.SqlQuery<QueryResult>("select [dbo].G2J(GETDATE()) as date,convert(varchar(8), GETDATE(), 108) as time").Single();
                this.Date = DateTimeCurrent.date;
                this.Time = DateTimeCurrent.time;
                //گرفتن تاریخ و ساعت جاری 

                this.Create(this);
                Timer();
                return "لینک فعال سازی به ایمیل شما ارسال شده است مدت زمان اعتبار لینک فعال سازی 1 ساعت می باشد، لطفا ایمیل خود را بررسی نمایید";
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return "در هنگام ارسال لینک فعال سازی مشکلی به وجود آمده است";
            }
        }
        //Timer Code confirm
        public static void Timer()
        {
            aTimer.Elapsed += new ElapsedEventHandler(DeleteRecordNotActive);
            aTimer.Interval = 3600000;
            aTimer.Enabled = true;
        }
        // پس از دوساعت اگر کاربر نسبت به فعالسازی حساب کاربری اقدام نکند اطلاعات آن حذف میشود
        public static void DeleteRecordNotActive(object source, ElapsedEventArgs e)
        {

            entity.User.RemoveRange(entity.User.Where(x => x.Active == false));
            try
            {
                entity.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            aTimer.Enabled = false;
        }
        public bool CheckCodeConfirm(string Code)
        {

            if(entity.User.Any(item => item.CodeConfirm == Code))
            {
                var obj = entity.User.Where(x => x.CodeConfirm == Code).Single();
                obj.Active = true;
                return true;
            }
            return false;
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
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        var error = "Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage;
                    }
                }
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

    
        public string Delete(int ID)
        {
            if (entity.User.Find(ID) != null)
            {
                entity.User.Remove(entity.User.Find(ID));
                try
                {
                    entity.SaveChanges();
                    return "اطلاعات با موفقیت حذف شد";
                }
                catch (Exception)
                {

                    return "در حذف اطلاعات مشکلی رخ داده است";
                }
            }
            else
            {
                return "";
            }
        }
        #endregion
    }


}
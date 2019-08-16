using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
#region khodadadi
using Rejime.Models;

//for new token for link comfirm email
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
#endregion

namespace Rejime.Models
{
    public  class DALS
    {
        #region khodadadi
        public static EF Entity = new EF();
        public static Menu ObjMenu = new Menu();
        public static User ObjUser = new User();
        public static string NewToken()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBuffer = new byte[16];
                rng.GetBytes(randomBuffer);

                using (MD5 md5 = MD5.Create())
                {
                    byte[] hashBytes = md5.ComputeHash(randomBuffer);

                    StringBuilder sBuilder = new StringBuilder();
                    foreach (byte byt in hashBytes)
                    {
                        sBuilder.Append(byt.ToString("x2"));
                    }

                    return sBuilder.ToString();
                }
            }
        }
        public class QueryResult
        {
            public string date { get; set; }
            public string time { get; set; }
        }
     public static QueryResult GetDateTime(string  para)
        {
            if (para == "before")
                return Entity.Database.SqlQuery<QueryResult>("select [dbo].G2J(DATEADD(HOUR, -2, GETDATE())) as date,convert(varchar(8),DATEADD(HOUR, -2, GETDATE()), 108) as time").Single();
            else
                return Entity.Database.SqlQuery<QueryResult>("select [dbo].G2J(GETDATE()) as date,convert(varchar(8), GETDATE(), 108) as time").Single();

        }
        #endregion
    }
}
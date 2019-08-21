using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Rejime.Models
{
    public partial class News
    {

        [Key]
        public int ID { get; set; }
        [MaxLength(80)]
        public string Title { get; set; }
        public string Desc { get; set; }
        [MaxLength(80)]
        public string ImageUrl { get; set; }
        [MaxLength(20)]
        public string Author { get; set; }
        [MaxLength(10)]
        public string InsertDate { get; set; }
        [MaxLength(10)]
        public string ExpirationDate { get; set; }
        [MaxLength(300)]
        public string Keyword { get; set; }
        public int VisitCount { get; set; }

    }
    public partial class News
    {
        EF entity = new EF();
        public string Create(News newRecord)
        {

            entity.News.Add(newRecord);
            try { entity.SaveChanges(); return "OK"; }
            catch
            {
                return "NOK";
            }
        }

        public List<News> Read()
        {
            return entity.News.ToList();

        }
        public News Read(int id)
        {
            return entity.News.Find(id);
        }

        public List<News> Read(int pIndex, int pSize)
        {
            return entity.News.OrderBy(item => item.ID).Skip((pIndex - 1) * pSize).Take(pSize).ToList();
        }

        public int Count()
        {
            return entity.News.Count();
        }
        public string Update(News newRecord)
        {
            entity.News.Attach(newRecord);
            entity.Entry(newRecord).State = EntityState.Modified;
            try { entity.SaveChanges(); return "OK"; }
            catch { return "NOK"; }
        }
        public string Delete(int id)
        {
            entity.News.Remove(entity.News.Find(id));
            try { entity.SaveChanges(); return "OK"; }
            catch { return "NOK"; }
        }

        public string Delete()
        {
            entity.News.RemoveRange(entity.News.ToList());
            try { entity.SaveChanges(); return "OK"; }
            catch { return "NOK"; }
        }

    }
}
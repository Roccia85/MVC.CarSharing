using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CarSharing.Models
{
    public class dbContext
    {
        public static ApplicationDbContext db
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (!HttpContext.Current.Items.Contains("dbContex"))
                    {
                        HttpContext.Current.Items["dbContex"] = ApplicationDbContext.Create();
                    }
                    return (ApplicationDbContext)HttpContext.Current.Items["dbContex"];
                }
                else return ApplicationDbContext.Create();
            }
            set
            {
                db = value;
            }
        }

        private static DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class
        {
            return db.Set<TEntity>();
        }

        public static IQueryable<TEntity> GetAll<TEntity>() where TEntity : class
        {
            IQueryable<TEntity> query = GetDbSet<TEntity>();
            return query;
        }
    }
}
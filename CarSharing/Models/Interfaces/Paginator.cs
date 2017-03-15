using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace CarSharing.Models
{
    public class Paginator<T> : IPaginator<T> where T: class
    {

        #region Internal
        protected Expression<Func<T, object>> orderBy;
        protected Expression<Func<T, object>> orderByNum;
        protected Expression<Func<T, object>> orderByDate;
        #endregion

        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems {
            get
            {
                return Filter == null ? dbContext.GetAll<T>().Count() : dbContext.GetAll<T>().Where(Filter).Count();
            }
        }
        public int TotalPages
        {
            get
            {
                double tot = TotalItems;
                return (int)(Math.Ceiling(tot / ItemsPerPage));
            }
        }
        public string OrderDirection { get; set; }
        public string OrderField { get; set; }
        public List<T> Items { get; set; }
        public Expression<Func<T, bool>> Filter { get; set; }

        public Expression<Func<T, object>> OrderBy { get { return orderBy; } set { orderByNum = null; orderByDate = null; orderBy = value; } }
        public Expression<Func<T, int>> OrderByNum { get; set; }
        public Expression<Func<T, DateTime>> OrderByDate { get; set; }

        public Paginator()
        {

        }

        public Paginator(int page) { }
        public Paginator(int page, int itemsPerPage) { }

        public IPaginator<T> Asc()
        {
            OrderDirection = "asc";
            return this;
        }

        public IPaginator<T> Desc()
        {
            OrderDirection = "desc";
            return this;
        }

        public IPaginator<T> Page(int page)
        {
            CurrentPage = page;
            return this;
        }

        public virtual void SetItems(int page = 1, int itemsPerPage = 10)
        {
            CurrentPage = page;
            ItemsPerPage = itemsPerPage;
            IQueryable<T> temp;

            if (OrderBy != null && OrderDirection.ToLower() == "asc")
            {
                if (Filter == null) temp = dbContext.GetAll<T>().OrderBy(OrderBy).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.GetAll<T>().Where(Filter).OrderBy(OrderBy).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderBy != null && OrderDirection.ToLower() == "desc")
            {
                if (Filter == null) temp = dbContext.GetAll<T>().OrderByDescending(OrderBy).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.GetAll<T>().Where(Filter).OrderByDescending(OrderBy).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderByNum != null && OrderDirection.ToLower() == "asc")
            {
                if (Filter == null) temp = dbContext.GetAll<T>().OrderBy(OrderByNum).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.GetAll<T>().Where(Filter).OrderBy(OrderByNum).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderByNum != null && OrderDirection.ToLower() == "desc")
            {
                if (Filter == null) temp = dbContext.GetAll<T>().OrderByDescending(OrderByNum).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.GetAll<T>().Where(Filter).OrderByDescending(OrderByNum).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderByDate != null && OrderDirection.ToLower() == "asc")
            {
                if (Filter == null) temp = dbContext.GetAll<T>().OrderBy(OrderByDate).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.GetAll<T>().Where(Filter).OrderBy(OrderByDate).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderByDate != null && OrderDirection.ToLower() == "desc")
            {
                if (Filter == null) temp = dbContext.GetAll<T>().OrderByDescending(OrderByDate).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.GetAll<T>().Where(Filter).OrderByDescending(OrderByDate).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else {
                Expression<Func<T, object>> ob = o => o.ToString();
                if (Filter == null) temp = dbContext.GetAll<T>().Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.GetAll<T>().Where(Filter).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }

            if(Filter == null) 


            Items = temp.ToList();

        }
    }
}
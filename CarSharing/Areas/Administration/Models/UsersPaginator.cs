using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace CarSharing.Models
{
    public class UsersPaginator : Paginator<ApplicationUser>
    {

        public override void SetItems(int page = 1, int itemsPerPage = 10)
        {
            CurrentPage = page;
            ItemsPerPage = itemsPerPage;
            IQueryable<ApplicationUser> temp;

            if (OrderBy != null && OrderDirection.ToLower() == "asc")
            {
                if (Filter == null) temp = dbContext.db.Users.OrderBy(OrderBy).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.db.Users.Where(Filter).OrderBy(OrderBy).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderBy != null && OrderDirection.ToLower() == "desc")
            {
                if (Filter == null) temp = dbContext.db.Users.OrderByDescending(OrderBy).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.db.Users.Where(Filter).OrderByDescending(OrderBy).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderByNum != null && OrderDirection.ToLower() == "asc")
            {
                if (Filter == null) temp = dbContext.db.Users.OrderBy(OrderByNum).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.db.Users.Where(Filter).OrderBy(OrderByNum).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderByNum != null && OrderDirection.ToLower() == "desc")
            {
                if (Filter == null) temp = dbContext.db.Users.OrderByDescending(OrderByNum).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.db.Users.Where(Filter).OrderByDescending(OrderByNum).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderByDate != null && OrderDirection.ToLower() == "asc")
            {
                if (Filter == null) temp = dbContext.db.Users.OrderBy(OrderByDate).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.db.Users.Where(Filter).OrderBy(OrderByDate).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else if (OrderByDate != null && OrderDirection.ToLower() == "desc")
            {
                if (Filter == null) temp = dbContext.db.Users.OrderByDescending(OrderByDate).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.db.Users.Where(Filter).OrderByDescending(OrderByDate).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }
            else
            {
                Expression<Func<ApplicationUser, object>> ob = o => o.UserName;
                if (Filter == null) temp = dbContext.db.Users.OrderBy(ob).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
                else temp = dbContext.db.Users.Where(Filter).OrderBy(ob).Skip(ItemsPerPage * (CurrentPage - 1)).Take(ItemsPerPage);
            }

            Items = temp.ToList();
        }
    }
}
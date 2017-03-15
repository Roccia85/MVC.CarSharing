using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace CarSharing.Models
{
    public interface IPaginator<T> where T: class
    {
        int CurrentPage { get; set; }
        int ItemsPerPage { get; set; }
        int TotalItems { get; }
        int TotalPages { get; }
        string OrderDirection { get; set; }
        string OrderField { get; set; }
        List<T> Items { get; set; }
        Expression<Func<T, bool>> Filter { get; set; }
        Expression<Func<T, object>> OrderBy { get; set; }
        Expression<Func<T, int>> OrderByNum { get; set; }
        Expression<Func<T, DateTime>> OrderByDate { get; set; }

        void SetItems(int page = 1, int itemsPerPage = 10);
        IPaginator<T> Asc();
        IPaginator<T> Desc();
        IPaginator<T> Page(int page);
    }
}

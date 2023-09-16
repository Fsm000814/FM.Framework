using Abp;
using DapperExtensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FM.FrameWork.Database
{
    internal static class SortingExtensions
    {
        public static List<ISort> ToSortable<T>(this Expression<Func<T, object>>[] sortingExpression, bool ascending = true)
        {
            Check.NotNullOrEmpty(sortingExpression, "sortingExpression");
            List<ISort> sortList = new List<ISort>();
            sortingExpression.ToList().ForEach(delegate (Expression<Func<T, object>> sortExpression)
            {
                MemberInfo property = ReflectionHelper.GetProperty(sortExpression);
                sortList.Add(new Sort
                {
                    Ascending = ascending,
                    PropertyName = property.Name
                });
            });
            return sortList;
        }
    }
}

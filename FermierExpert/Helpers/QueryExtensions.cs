using System.Collections.Generic;
using System.Linq;

namespace FermierExpert.Helpers
{
    public static class QueryExtensions
    {
        public static IOrderedEnumerable<T> OrderByColumns<T>(this IEnumerable<T> source, string[] sortColumns)
        {
            var sortedList = source.OrderBy(x => true);
            if (sortColumns != null)
            {
                foreach (var sortColumn in sortColumns)
                {
                    var prop = typeof(T).GetProperties().FirstOrDefault(p => p.Name.ToLower() == sortColumn.ToLower());
                    if (prop != null)
                    {
                        sortedList = sortedList.ThenBy(x => prop.GetValue(x));
                    }
                };
            }
            return sortedList;
        }
    }
}

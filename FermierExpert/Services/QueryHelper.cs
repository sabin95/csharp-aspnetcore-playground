using FermierExpert.Services;
using FermierExpert.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FermierExpert.Services
{
    public class QueryHelper : IQueryHelper
    {
        private readonly Contracts.IComparer<DateTime> _dateTimeComparer;
        private readonly Contracts.IComparer<string> _stringComparer;
        private readonly Contracts.IComparer<float> _floatComparer;
        private readonly Contracts.IComparer<int> _intComparer;
        private readonly Contracts.IComparer<Guid> _guidComparer;
        private readonly Contracts.IComparer<long> _longComparer;

        public QueryHelper(Contracts.IComparer<int> intComparer, Contracts.IComparer<Guid> guidComparer, Contracts.IComparer<DateTime> dateTimeComparer, Contracts.IComparer<string> stringComparer, Contracts.IComparer<float> floatComparer, Contracts.IComparer<long> longComparer)
        {
            _intComparer = intComparer;
            _guidComparer = guidComparer;
            _dateTimeComparer = dateTimeComparer;
            _stringComparer = stringComparer;
            _floatComparer = floatComparer;
            _longComparer = longComparer;
        }
        private bool Compare<T>(T a, T b)
        {
            var props = a.GetType().GetProperties();
            foreach (var prop in props)
            {
                var valueA = prop.GetValue(a);
                var valueB = prop.GetValue(b);
                if (prop.PropertyType == typeof(int) || prop.PropertyType.IsEnum)
                {
                    return _intComparer.Compare((int)valueA,(int)valueB);
                }
                if (prop.PropertyType == typeof(float))
                {
                    return _floatComparer.Compare((float)valueA, (float)valueB);
                }
                if (prop.PropertyType == typeof(long))
                {
                    return _longComparer.Compare((long)valueA,(long)valueB);
                }
                if (prop.PropertyType == typeof(Guid))
                {
                    return _guidComparer.Compare((Guid)valueA,(Guid)valueB);
                }
                if (prop.PropertyType == typeof(string))
                {
                    return _stringComparer.Compare(valueA.ToString(),valueB.ToString());
                }
                if (prop.PropertyType == typeof(DateTime))
                {
                    return _dateTimeComparer.Compare((DateTime)valueA,(DateTime)valueB);
                }
            }
            return true;
        }

        public IEnumerable<T> WhereByColumns<T>(IEnumerable<T> source, T compareTo)
        {
            if (compareTo != null)
            {
                var props = compareTo.GetType().GetProperties();
                foreach (var prop in props)
                {
                    source = source.Where(x => Compare(x, compareTo));
                }
            }
            return source;
        }

        public IEnumerable<T> Slice<T>(IEnumerable<T> source, int start, int count)
        {
            if (start > 0 && count > 0)
            {
                var slicedSource = source.Skip(start)
                                .Take(count);
                return slicedSource;
            }
            return source;
        }

        public IOrderedEnumerable<T> OrderByColumns<T>(IEnumerable<T> source, string[] sortColumns)
        {
            var sortedList = source.OrderBy(x => true);
            if (sortColumns != null && sortColumns.Count() > 0)
            {
                
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
            }
            return sortedList;
        }
    }
}

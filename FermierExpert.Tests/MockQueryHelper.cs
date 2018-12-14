using System;
using System.Collections.Generic;
using System.Linq;
using FermierExpert.Services.Contracts;

namespace FermierExpert.Tests
{
    public class MockQueryHelper : IQueryHelper
    {
        public IOrderedEnumerable<T> OrderByColumns<T>(IEnumerable<T> source, string[] sortColumns)
        {
            return source.OrderBy(x => true);
        }

        public IEnumerable<T> Slice<T>(IEnumerable<T> source, int start, int count)
        {
            return source;
        }

        public IEnumerable<T> WhereByColumns<T>(IEnumerable<T> source, T compareTo)
            where T : class
        {
            return source;
        }
    }
}

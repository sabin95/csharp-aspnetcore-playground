using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FermierExpert.Services.Contracts
{
    public interface IQueryHelper
    {
        IEnumerable<T> WhereByColumns<T>(IEnumerable<T> source, T compareTo);
        IEnumerable<T> Slice<T>(IEnumerable<T> source, int start, int count);
        IOrderedEnumerable<T> OrderByColumns<T>(IEnumerable<T> source, string[] sortColumns);
    }
}

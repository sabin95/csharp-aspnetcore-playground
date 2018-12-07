using FermierExpert.Services.Contracts;
using System;

namespace FermierExpert.Services
{
    public class IntComparer : IComparer<int>
    {
        public bool Compare(int a, int b)
        {
            return a == b;
        }
    }

    public class FloatComparer : IComparer<float>
    {
        public bool Compare(float a, float b)
        {
            return a == b;
        }
    }

    public class LongComparer : IComparer<long>
    {
        public bool Compare(long a, long b)
        {
            return a == b;
        }
    }

    public class GuidComparer : IComparer<Guid>
    {
        public bool Compare(Guid a, Guid b)
        {
            return a == b;
        }
    }

    public class DateTimeComparer : IComparer<DateTime>
    {
        public bool Compare(DateTime a, DateTime b)
        {
            return a == b;
        }
    }

    public class StringComparer : IComparer<string>
    {
        public bool Compare(string a, string b)
        {
            return a.Contains(b);
        }
    }

}

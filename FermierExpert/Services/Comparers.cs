using FermierExpert.Models;
using FermierExpert.Services.Contracts;
using System;

namespace FermierExpert.Services
{
    public class IntComparer : IComparer<int>
    {
        public bool Compare(int a, int b)
        {
            if (b == 0)
            {
                return true;
            }
            return a == b;
        }
    }

    public class EnumComparer : IComparer<Enum>
    {
        public bool Compare(Enum a, Enum b)
        {
            var values = Enum.GetValues(b.GetType());
            if (values.Length <= 0)
            {
                return true;
            }
            var first = values.GetValue(0);
            if (b.Equals(first))
            {
                return true;
            }
            return a.Equals(b);
        }
    }

    public class FloatComparer : IComparer<float>
    {
        public bool Compare(float a, float b)
        {
            if (b == 0)
            {
                return true;
            }
            return a == b;
        }
    }

    public class LongComparer : IComparer<long>
    {
        public bool Compare(long a, long b)
        {
            if (b == 0)
            {
                return true;
            }
            return a == b;
        }
    }

    public class GuidComparer : IComparer<Guid>
    {
        public bool Compare(Guid a, Guid b)
        {
            if (b == Guid.Empty)
            {
                return true;
            }
            return a == b;
        }
    }

    public class DateTimeComparer : IComparer<DateTime>
    {
        public bool Compare(DateTime a, DateTime b)
        {
            if (b == DateTime.MinValue)
            {
                return true;
            }
            return a == b;
        }
    }

    public class StringComparer : IComparer<string>
    {
        public bool Compare(string a, string b)
        {
            if (b is null)
            {
                return true;
            }
            return a.Contains(b);
        }
    }

}

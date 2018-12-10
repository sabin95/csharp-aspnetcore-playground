using FermierExpert.Services;
using FermierExpert.Services.Contracts;
using StringComparer = FermierExpert.Services.StringComparer;

namespace FermierExpert.Tests
{
    public static class MockQueryHelperFactory
    {
        public static IQueryHelper Create()
        {
            return new QueryHelper(new IntComparer(), new GuidComparer(),
                                new DateTimeComparer(), new StringComparer(),
                                new FloatComparer(), new LongComparer(), new EnumComparer());
        }
    }
}

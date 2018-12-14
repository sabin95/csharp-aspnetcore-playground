using System;
using FermierExpert.Services;
using Xunit;

namespace FermierExpert.Tests.Services.QueryHelperTests
{
    public class SliceTests
    {
        [Fact]
        public void Slide_Should_Return_Null_On_Null_Source()
        {
            var queryHelper = MockQueryHelperFactory.Create();

            var slice = queryHelper.Slice<int>(null, 3, 2);

            Assert.Null(slice);
        }

        [Fact]
        public void Slice_Should_Throw_Argument_Exception_On_Negative_Start()
        {
            var list = new int[] { 1, 2, 3, 4, 5 };
            var queryHelper = MockQueryHelperFactory.Create();


            Assert.Throws<ArgumentException>(() => queryHelper.Slice(list, -1, 2));
        }

        [Fact]
        public void Slice_Should_Throw_Argument_Exception_On_Negatve_Count()
        {
            var list = new int[] { 1, 2, 3, 4, 5 };
            var queryHelper = MockQueryHelperFactory.Create();

            Assert.Throws<ArgumentException>(() => queryHelper.Slice(list, 1, -2));
        }
        [Fact]
        public void Slice_Should_Return_FullList_On_Greater_Count()
        {
            var list = new int[] { 1, 2, 3, 4, 5 };
            var queryHelper = MockQueryHelperFactory.Create();

            var slice = queryHelper.Slice(list, 0, 10);

            Assert.Equal(slice, list);
        }

        [Fact]
        public void Slide_Should_Return_First_two_elements()
        {
            var list = new int[] { 1, 2, 3, 4, 5 };
            var queryHelper = MockQueryHelperFactory.Create();

            var slice = queryHelper.Slice(list, 0, 2);

            Assert.Equal(slice, new int[] { 1, 2 });
        }

        [Fact]
        public void Slide_Should_Return_Last_two_elements()
        {
            var list = new int[] { 1, 2, 3, 4, 5 };
            var queryHelper = MockQueryHelperFactory.Create();

            var slice = queryHelper.Slice(list, 3, 2);

            Assert.Equal(slice, new int[] { 4, 5 });
        }
    }
}

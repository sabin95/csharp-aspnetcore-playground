using FermierExpert.Models;
using FermierExpert.Services;
using System.Linq;
using Xunit;

namespace FermierExpert.Tests.Services.QueryHelperTests
{
    public class OrderTests
    {
        [Fact]
        public void Order_Should_Return_Ordered_By_FirstName()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedList = list.Clients.OrderBy(x => x.FirstName);
            var orderedListCustom = queryHelper.OrderByColumns(list.Clients, new string[] { "firstName" });
            Assert.Equal(orderedListCustom, orderedList);
        }

        [Fact]
        public void Order_Should_Return_Ordered_By_Name_ThenBy_LastName()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedList = list.Clients.OrderBy(x => x.FirstName).
                                            ThenBy(x => x.LastName);
            var orderedListCustom = queryHelper.OrderByColumns(list.Clients, new string[] { "firstName", "lastname" });
            Assert.Equal(orderedListCustom, orderedList);
        }

        [Fact]
        public void Order_Should_Return_Ordered_By_Name_ThenBy_LastName_thenBy_language()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedList = list.Clients.OrderBy(x => x.FirstName).
                                            ThenBy(x => x.LastName)
                                            .ThenBy(x => x.Language);
            var orderedListCustom = queryHelper.OrderByColumns(list.Clients, new string[] { "firstName", "lastname", "language" });
            Assert.Equal(orderedListCustom, orderedList);
        }

        [Fact]
        public void Order_Should_Return_Ordered_By_Name_ThenBy_LastName_thenBy_language_thenby_id()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedList = list.Clients.OrderBy(x => x.FirstName)
                                            .ThenBy(x => x.LastName)
                                            .ThenBy(x => x.Language)
                                            .ThenBy(x => x.Id);
            var orderedListCustom = queryHelper.OrderByColumns(list.Clients, new string[] { "firstName", "lastname", "language", "id" });
            Assert.Equal(orderedListCustom, orderedList);
        }

        [Fact]
        public void Order_Should_Return_Same_On_Non_Existing_properties()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedList = list.Clients;
            var orderedListCustom = queryHelper.OrderByColumns(list.Clients, new string[] { "fdsfsd", "fsagfd" });
            Assert.Equal(orderedListCustom, orderedList);
        }


        [Fact]
        public void Order_Should_Return_Same_On_Null_Source()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedListCustom = queryHelper.OrderByColumns<Client>(null, null);
            Assert.Null(orderedListCustom);
        }

        [Fact]
        public void Order_Should_Return_Same_On_Null_Sort_Columns_Array()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedList = list.Clients;
            var orderedListCustom = queryHelper.OrderByColumns(list.Clients, null);
            Assert.Equal(orderedListCustom, orderedList);
        }

        [Fact]
        public void Order_Should_Return_Same_On_Empty_Sort_Columns_Array()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedList = list.Clients;
            var orderedListCustom = queryHelper.OrderByColumns(list.Clients, new string[] { });
            Assert.Equal(orderedListCustom, orderedList);
        }

        [Fact]
        public void Order_Should_Return_Same_Ordered_by_language_on_invalid_property_and_language()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var orderedList = list.Clients.OrderBy(x => x.Language);
            var orderedListCustom = queryHelper.OrderByColumns(list.Clients, new string[] { "fdsfsd", "language" });
            Assert.Equal(orderedListCustom, orderedList);
        }
    }
}

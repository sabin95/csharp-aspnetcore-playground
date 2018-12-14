using System.Linq;
using FermierExpert.Models;
using Xunit;

namespace FermierExpert.Tests.Services.QueryHelperTests
{
    public class FilterTests
    {

        [Fact]
        public void Filter_Should_Return_Same_On_Null_Source_Parameter()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var filteredListCustom = queryHelper.WhereByColumns(null, new Client());
            Assert.Null(filteredListCustom);
        }

        [Fact]
        public void Filter_Should_Return_Same_On_Null_Filter_Parameter()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var filteredList = list.Clients;
            var filteredListCustom = queryHelper.WhereByColumns(list.Clients, null).ToList();
            Assert.Equal(filteredListCustom, filteredList);
        }

        [Fact]
        public void Filter_Should_Return_Same_On_No_Filter()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var filteredList = list.Clients;
            var clientToFilter = new Client();
            var filteredListCustom = queryHelper.WhereByColumns(list.Clients, clientToFilter).ToList();
            Assert.Equal(filteredListCustom, filteredList);
        }

        [Fact]
        public void Filter_Should_Return_Filtered_By_Id()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var filteredList = list.Clients.Where(x => x.Id == 2).ToList();
            var clientToFilter = new Client
            {
                Id = 2
            };
            var filteredListCustom = queryHelper.WhereByColumns(list.Clients, clientToFilter).ToList();
            Assert.Equal(filteredListCustom, filteredList);
        }

        [Fact]
        public void Filter_Should_Return_Filtered_By_FirstName_And_FastName()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var filteredList = list.Clients.Where(x => x.FirstName.Contains("i") && x.LastName.Contains("e")).ToList();
            var clientToFilter = new Client
            {
                FirstName = "i",
                LastName = "e"

            };
            var filteredListCustom = queryHelper.WhereByColumns(list.Clients, clientToFilter).ToList();
            Assert.Equal(filteredListCustom, filteredList);
        }

        [Fact]
        public void Filter_Should_Return_Filtered_By_FirstName_And_FastName_And_DOB()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var filteredList = list.Clients.Where(x => x.FirstName.Contains("i") && x.LastName.Contains("e") && x.DateOfBirth == new System.DateTime(1995, 12, 1)).ToList();
            var clientToFilter = new Client
            {
                FirstName = "i",
                LastName = "e",
                DateOfBirth = new System.DateTime(1995, 12, 1)

            };
            var filteredListCustom = queryHelper.WhereByColumns(list.Clients, clientToFilter).ToList();
            Assert.Equal(filteredListCustom, filteredList);
        }

        [Fact]
        public void Filter_Should_Return_Filtered_By_Language()
        {
            var list = MockDatabaseFactory.Create();
            var queryHelper = MockQueryHelperFactory.Create();
            var filteredList = list.Clients.Where(x => x.Language == SpokenLanguage.English).ToList();
            var clientToFilter = new Client
            {
                Language = SpokenLanguage.English

            };
            var filteredListCustom = queryHelper.WhereByColumns(list.Clients, clientToFilter).ToList();
            Assert.Equal(filteredListCustom, filteredList);
        }
    }
}

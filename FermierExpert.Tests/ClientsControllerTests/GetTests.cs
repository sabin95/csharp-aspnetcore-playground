using FermierExpert.Controllers;
using FermierExpert.Queries;
using System.Linq;
using Xunit;

namespace FermierExpert.Tests.ClientsControllerTests
{
    public class GetTests
    {
        [Fact]
        void GetAll_Should_Sort_For_One_Column()
        {
            var db = MockDatabaseFactory.CreateNewDatabase();
            var controller = new ClientsController(db);
            var columnsToSort = new string[] { "id" };
            var expected = db.Clients
                .OrderBy(x => x.Id)
                .ToList();

            var actual = controller.GetAll(new GetAllClientsQuery
            {
                Start = 0,
                Count = db.Clients.Count,
                SortColumns = columnsToSort
            }).ToList();

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }

        [Fact]
        void GetAll_Should_Sort_For_Two_Columns()
        {
            var db = MockDatabaseFactory.CreateNewDatabase();
            var controller = new ClientsController(db);
            var columnsToSort = new string[] { "id","lastname" };
            var expected = db.Clients
                .OrderBy(x => x.Id)
                .ThenBy(x=>x.LastName)
                .ToList();

            var actual = controller.GetAll(new GetAllClientsQuery
            {
                Start = 0,
                Count = db.Clients.Count,
                SortColumns = columnsToSort
            }).ToList();

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }

        [Fact]
        void GetAll_Should_Sort_For_Tree_Columns()
        {
            var db = MockDatabaseFactory.CreateNewDatabase();
            var controller = new ClientsController(db);
            var columnsToSort = new string[] { "id", "lastname","firstname" };
            var expected = db.Clients
                .OrderBy(x => x.Id)
                .ThenBy(x => x.LastName)
                .ThenBy(x=>x.FirstName)
                .ToList();

            var actual = controller.GetAll(new GetAllClientsQuery
            {
                Start = 0,
                Count = db.Clients.Count,
                SortColumns = columnsToSort
            }).ToList();

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }

        [Fact]
        void GetAll_Should_Sort_For_Four_Columns()
        {
            var db = MockDatabaseFactory.CreateNewDatabase();
            var controller = new ClientsController(db);
            var columnsToSort = new string[] { "language", "lastname", "firstname","id" };
            var expected = db.Clients
                .OrderBy(x => x.Language)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ThenBy(x=>x.Id)
                .ToList();

            var actual = controller.GetAll(new GetAllClientsQuery
            {
                Start = 0,
                Count = db.Clients.Count,
                SortColumns = columnsToSort
            }).ToList();

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i].Id, actual[i].Id);
            }
        }
    }
}

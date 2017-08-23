using Microsoft.VisualStudio.TestTools.UnitTesting;
using Grubber.Mapper;
using System.Data;
using System.Data.SqlClient;

namespace Grubber.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Builder builder = new Builder();

            builder.AddMap(new TestModelMapper());

            TestModel model = new TestModel();
            model.Username = "TestUser";

            IDbConnection db = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DapperWrapper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

            var created = builder.Create(model, db);
        }
    }
}

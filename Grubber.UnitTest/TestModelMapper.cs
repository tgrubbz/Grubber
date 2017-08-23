using Grubber.Mapper;
using System.Data;

namespace Grubber.UnitTest
{
    public class TestModelMapper : BaseMapper<TestModel>
    {
        public TestModelMapper()
            : base("Users")
        {
            Map(x => x.Id).Type(DbType.Int32).IsKey();
            Map(x => x.Username).Type(DbType.AnsiString).Size(50);
        }
    }
}

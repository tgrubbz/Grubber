using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grubber
{
    public static class DbTypeResolver
    {
        public static DbType Resolve(int type)
        {
            return DbType.Int32;
        }
        public static DbType Resolve(string type)
        {
            return DbType.Int32;
        }
    }
}

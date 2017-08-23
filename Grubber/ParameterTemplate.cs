using System.Data;
using System.Reflection;

namespace Grubber
{
    public class ParameterTemplate
    {
        public string Name { get; set; }
        public DbType? Type { get; set; }
        public int? Size { get; set; }

        public PropertyInfo Propertyinfo { get; set; }

        public ParameterTemplate(string name)
        {
            Name = name;
        }
    }
}

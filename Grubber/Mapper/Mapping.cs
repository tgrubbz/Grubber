using System;
using System.Data;

namespace Grubber.Mapper
{
    public class Mapping
    {
        public PropertyModel Model { get; set; }

        public Mapping(string name, Type type)
        {
            Model = new PropertyModel(name);
            Model.Propertyinfo = type.GetProperty(name);
        }

        public Mapping Type(DbType type)
        {
            Model.Type = type;
            return this;
        }

        public Mapping Size(int size)
        {
            Model.Size = size;
            return this;
        }

        public Mapping Name(string name)
        {
            Model.Name = name;
            return this;
        }

        public Mapping Column(string column)
        {
            Model.Column = column;
            return this;
        }

        public Mapping IsKey()
        {
            Model.IsKey = true;
            return this;
        }
    }
}

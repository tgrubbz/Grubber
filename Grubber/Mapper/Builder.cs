using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Grubber.Mapper
{
    public class Builder
    {
        Dictionary<Type, IEnumerable<PropertyModel>> ModelDict = new Dictionary<Type, IEnumerable<PropertyModel>>();
        Dictionary<Type, string> Tables = new Dictionary<Type, string>();

        public void AddMap<TClass>(BaseMapper<TClass> mapper) where TClass : class
        {
            Type type = typeof(TClass);

            ModelDict.Add(type, mapper.Models);
            Tables.Add(type, mapper.Table);
        }

        private Type Validate<TClass>(TClass item) where TClass : class
        {
            Type type = typeof(TClass);
            if (!ModelDict.ContainsKey(type) || !ModelDict[type].Any())
            {
                throw new ArgumentException("No mapping found for the type: " + type.Name + ". Make sure the map has been added to the builder.");
            }
            return type;
        }

        public DynamicParameters Build<TClass>(TClass item) where TClass : class
        {
            Type type = Validate(item);

            // Get the parameter templates
            DynamicParameters parameters = new DynamicParameters();
            IEnumerable<PropertyModel> models = ModelDict[type];
            foreach(var model in models)
            {
                // Get the value of the mapped property
                object value = model.Propertyinfo.GetValue(item);

                // Add to the parameters
                parameters.Add(model.Name, value, model.Type, size: model.Size);
            }
            return parameters;
        }

        public void Create<TClass>(TClass item, IDbConnection db) where TClass : class
        {
            // TODO: Primary Key columns must have values
            Type type = Validate(item);

            IEnumerable<PropertyModel> models = ModelDict[type];
            string sql = "INSERT INTO [" + Tables[type] + "]";

            PropertyModel identityColumn = models.SingleOrDefault(x => x.IsIdentity);
            List<string> columns = new List<string>();
            List<string> values = new List<string>();
            foreach (var model in models.Where(x => !x.IsIdentity))
            {
                columns.Add(model.Column);
                values.Add("@" + model.Name);
            }

            sql += " (" + string.Join(", ", columns) + ")";
            if(identityColumn != null)
            {
                sql += " OUTPUT INSERTED." + identityColumn.Column;
            }
            sql += " VALUES (" + string.Join(", ", values) + ")";

            if (identityColumn != null)
            {
                IDictionary<string, object> result;
                using (SqlConnection conn = new SqlConnection(db.ConnectionString))
                {
                    result = (IDictionary<string, object>)conn.Query(sql, Build(item)).SingleOrDefault();
                }
                identityColumn.Propertyinfo.SetValue(item, result[identityColumn.Column]);
            }
            else
            {
                using (SqlConnection conn = new SqlConnection(db.ConnectionString))
                {
                    conn.Execute(sql, Build(item));
                }
            }
        }
    }
}

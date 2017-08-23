using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Grubber.Mapper
{
    public abstract class BaseMapper<TClass> where TClass : class
    {
        private List<PropertyModel> models = new List<PropertyModel>();
        public IEnumerable<PropertyModel> Models { get { return models; } }

        private string table { get; set; }
        public string Table { get { return table; } }

        protected Mapping Map<TProperty>(Expression<Func<TClass, TProperty>> expression)
        {
            // Check for member expression
            if (expression.Body.NodeType != ExpressionType.MemberAccess)
            {
                throw new ArgumentException("Expression is not a member access expresssion");
            }

            // Get the name of the member in the expression
            MemberExpression memberExpression = expression.Body as MemberExpression;
            Mapping mapping = new Mapping(memberExpression.Member.Name, typeof(TClass));

            models.Add(mapping.Model);

            return mapping;
        }
        
        public BaseMapper(string tablename)
        {
            table = tablename;
        }
    }
}

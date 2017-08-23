namespace Grubber
{
    public class Parameter : ParameterTemplate
    {
        public object Value { get; set; }

        public Parameter(ParameterTemplate template, object value)
            : base(template.Name)
        {
            Type = template.Type;
            Size = template.Size;
            Value = value;
        }
    }
}

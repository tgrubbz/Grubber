namespace Grubber
{
    public class PropertyModel : ParameterTemplate
    {
        public PropertyModel(string name) : base(name)
        {
            Column = name;
        }

        public string Column { get; set; }
        public bool IsKey { get; set; }
    }
}

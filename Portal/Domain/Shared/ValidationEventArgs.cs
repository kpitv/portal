namespace Portal.Domain.Shared
{
    public class ValidationEventArgs
    {
        public ValidationError Name { get; }
        public string Property { get; }


        public ValidationEventArgs(ValidationError name, string property)
        {
            Name = name;
            Property = property;
        }
    }
}

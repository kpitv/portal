namespace Portal.Application.Errors
{
    public class ErrorEventArgs
    {
        public ApplicationError Name { get; }
        public string Property { get; }


        public ErrorEventArgs(ApplicationError name, string property)
        {
            Name = name;
            Property = property;
        }
    }
}

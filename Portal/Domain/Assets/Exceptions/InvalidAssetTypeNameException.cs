using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class InvalidAssetTypeNameException : DomainException
    {
        public InvalidAssetTypeNameException(string name) : base(name) { }
    }
}

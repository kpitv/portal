using Portal.Domain.Shared;

namespace Portal.Domain.Assets.Exceptions
{
    public class InvalidAssetTypeNameException : DomainException<string>
    {
        public InvalidAssetTypeNameException(string invalidValue) : base(invalidValue) { }
    }
}

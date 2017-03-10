using Portal.Domain.Shared;

namespace Portal.Domain.Assets
{
    public class Asset : AggregateRoot
    {
        public Type Type { get; set; }
    }
}

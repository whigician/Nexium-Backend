namespace Nexium.API.Entities;

public class BusinessRelationship
{
    public long BusinessId { get; set; }
    public Business Business { get; set; }

    public long RelatedBusinessId { get; set; }
    public Business RelatedBusiness { get; set; }

    public bool IsRetailer { get; set; }
}
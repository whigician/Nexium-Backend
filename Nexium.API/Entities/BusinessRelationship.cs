namespace Nexium.API.Entities;

public class BusinessRelationship
{
    public long SupplierId { get; set; }
    public Business Supplier { get; set; }

    public long RetailerId { get; set; }
    public Business Retailer { get; set; }
    public BusinessRelationshipType BusinessRelationshipType { get; set; }
}
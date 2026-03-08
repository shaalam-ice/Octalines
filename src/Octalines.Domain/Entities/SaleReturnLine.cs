using Volo.Abp.Domain.Entities;

namespace Octalines.Entities;

public class SaleReturnLine : Entity<Guid>
{
    public Guid SaleReturnId { get; set; }
    public Guid ItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}

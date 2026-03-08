using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class Expense : FullAuditedAggregateRoot<Guid>
{
    public Guid ExpenseCategoryId { get; set; }
    public Guid? AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    public string? Reference { get; set; }
}

using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class ExpenseCategoryDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateUpdateExpenseCategoryDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class ExpenseDto : FullAuditedEntityDto<Guid>
{
    public Guid ExpenseCategoryId { get; set; }
    public string? CategoryName { get; set; }
    public Guid? AccountId { get; set; }
    public string? AccountName { get; set; }
    public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; }
    public string? Description { get; set; }
    public string? Reference { get; set; }
}

public class CreateUpdateExpenseDto
{
    [Required] public Guid ExpenseCategoryId { get; set; }
    public Guid? AccountId { get; set; }
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
    public DateTime ExpenseDate { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    public string? Reference { get; set; }
}

public class GetExpensesInput : PagedAndSortedResultRequestDto
{
    public Guid? CategoryId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}

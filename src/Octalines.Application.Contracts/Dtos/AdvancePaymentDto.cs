using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class AdvancePaymentDto : FullAuditedEntityDto<Guid>
{
    public Guid ContactId { get; set; }
    public string? ContactName { get; set; }
    public AdvanceType AdvanceType { get; set; }
    public decimal Amount { get; set; }
    public decimal UsedAmount { get; set; }
    public decimal RemainingAmount { get; set; }
    public DateTime AdvanceDate { get; set; }
    public string? Notes { get; set; }
}

public class CreateAdvancePaymentDto
{
    [Required] public Guid ContactId { get; set; }
    public AdvanceType AdvanceType { get; set; }
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
    public string? Notes { get; set; }
}

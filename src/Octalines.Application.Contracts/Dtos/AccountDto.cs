using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class AccountDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
    public string? Notes { get; set; }
}

public class CreateUpdateAccountDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public AccountType AccountType { get; set; }
    public string? Notes { get; set; }
    public bool IsActive { get; set; } = true;
}

public class MoneyTransferDto : FullAuditedEntityDto<Guid>
{
    public Guid FromAccountId { get; set; }
    public string? FromAccountName { get; set; }
    public Guid ToAccountId { get; set; }
    public string? ToAccountName { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransferDate { get; set; }
    public string? Notes { get; set; }
}

public class CreateMoneyTransferDto
{
    [Required] public Guid FromAccountId { get; set; }
    [Required] public Guid ToAccountId { get; set; }
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
    public string? Notes { get; set; }
}

public class DepositDto : FullAuditedEntityDto<Guid>
{
    public Guid AccountId { get; set; }
    public string? AccountName { get; set; }
    public decimal Amount { get; set; }
    public DateTime DepositDate { get; set; }
    public string? Notes { get; set; }
}

public class CreateDepositDto
{
    [Required] public Guid AccountId { get; set; }
    [Range(0.01, double.MaxValue)] public decimal Amount { get; set; }
    public string? Notes { get; set; }
}

using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Accounts.Default)]
public class AccountAppService : ApplicationService, IAccountAppService
{
    private readonly IRepository<Account, Guid> _accountRepo;
    private readonly IRepository<MoneyTransfer, Guid> _transferRepo;
    private readonly IRepository<Deposit, Guid> _depositRepo;

    public AccountAppService(
        IRepository<Account, Guid> accountRepo,
        IRepository<MoneyTransfer, Guid> transferRepo,
        IRepository<Deposit, Guid> depositRepo)
    {
        _accountRepo = accountRepo;
        _transferRepo = transferRepo;
        _depositRepo = depositRepo;
    }

    public async Task<PagedResultDto<AccountDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _accountRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<AccountDto>(total, ObjectMapper.Map<List<Account>, List<AccountDto>>(items));
    }

    public async Task<AccountDto> GetAsync(Guid id)
        => ObjectMapper.Map<Account, AccountDto>(await _accountRepo.GetAsync(id));

    [Authorize(OctalinesPermissions.Accounts.Create)]
    public async Task<AccountDto> CreateAsync(CreateUpdateAccountDto input)
    {
        var account = new Account(GuidGenerator.Create());
        ObjectMapper.Map(input, account);
        await _accountRepo.InsertAsync(account, autoSave: true);
        return ObjectMapper.Map<Account, AccountDto>(account);
    }

    [Authorize(OctalinesPermissions.Accounts.Edit)]
    public async Task<AccountDto> UpdateAsync(Guid id, CreateUpdateAccountDto input)
    {
        var account = await _accountRepo.GetAsync(id);
        ObjectMapper.Map(input, account);
        await _accountRepo.UpdateAsync(account, autoSave: true);
        return ObjectMapper.Map<Account, AccountDto>(account);
    }

    [Authorize(OctalinesPermissions.Accounts.Delete)]
    public async Task DeleteAsync(Guid id)
        => await _accountRepo.DeleteAsync(id, autoSave: true);

    public async Task<MoneyTransferDto> TransferAsync(CreateMoneyTransferDto input)
    {
        if (input.FromAccountId == input.ToAccountId)
            throw new UserFriendlyException("Cannot transfer to the same account.");

        var from = await _accountRepo.GetAsync(input.FromAccountId);
        var to = await _accountRepo.GetAsync(input.ToAccountId);

        if (from.Balance < input.Amount)
            throw new UserFriendlyException("Insufficient balance in source account.");

        from.Balance -= input.Amount;
        to.Balance += input.Amount;

        await _accountRepo.UpdateAsync(from);
        await _accountRepo.UpdateAsync(to);

        var transfer = new MoneyTransfer(GuidGenerator.Create())
            {
            FromAccountId = input.FromAccountId,
            ToAccountId = input.ToAccountId,
            Amount = input.Amount,
            TransferDate = DateTime.UtcNow,
            Notes = input.Notes
        };

        await _transferRepo.InsertAsync(transfer, autoSave: true);

        var dto = ObjectMapper.Map<MoneyTransfer, MoneyTransferDto>(transfer);
        dto.FromAccountName = from.Name;
        dto.ToAccountName = to.Name;
        return dto;
    }

    public async Task<PagedResultDto<MoneyTransferDto>> GetTransfersAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _transferRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.OrderByDescending(t => t.TransferDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        var accounts = await _accountRepo.GetListAsync();
        var dtos = items.Select(t =>
        {
            var dto = ObjectMapper.Map<MoneyTransfer, MoneyTransferDto>(t);
            dto.FromAccountName = accounts.FirstOrDefault(a => a.Id == t.FromAccountId)?.Name;
            dto.ToAccountName = accounts.FirstOrDefault(a => a.Id == t.ToAccountId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<MoneyTransferDto>(total, dtos);
    }

    public async Task<DepositDto> DepositAsync(CreateDepositDto input)
    {
        var account = await _accountRepo.GetAsync(input.AccountId);
        account.Balance += input.Amount;
        await _accountRepo.UpdateAsync(account);

        var deposit = new Deposit(GuidGenerator.Create())
            {
            AccountId = input.AccountId,
            Amount = input.Amount,
            DepositDate = DateTime.UtcNow,
            Notes = input.Notes
        };
        await _depositRepo.InsertAsync(deposit, autoSave: true);

        var dto = ObjectMapper.Map<Deposit, DepositDto>(deposit);
        dto.AccountName = account.Name;
        return dto;
    }

    public async Task<PagedResultDto<DepositDto>> GetDepositsAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _depositRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.OrderByDescending(d => d.DepositDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        var accounts = await _accountRepo.GetListAsync();
        var dtos = items.Select(d =>
        {
            var dto = ObjectMapper.Map<Deposit, DepositDto>(d);
            dto.AccountName = accounts.FirstOrDefault(a => a.Id == d.AccountId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<DepositDto>(total, dtos);
    }
}

using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IAccountAppService : IApplicationService
{
    Task<PagedResultDto<AccountDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<AccountDto> GetAsync(Guid id);
    Task<AccountDto> CreateAsync(CreateUpdateAccountDto input);
    Task<AccountDto> UpdateAsync(Guid id, CreateUpdateAccountDto input);
    Task DeleteAsync(Guid id);
    Task<MoneyTransferDto> TransferAsync(CreateMoneyTransferDto input);
    Task<PagedResultDto<MoneyTransferDto>> GetTransfersAsync(PagedAndSortedResultRequestDto input);
    Task<DepositDto> DepositAsync(CreateDepositDto input);
    Task<PagedResultDto<DepositDto>> GetDepositsAsync(PagedAndSortedResultRequestDto input);
}

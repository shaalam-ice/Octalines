using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/accounts")]
public class AccountController : AbpController, IAccountAppService
{
    private readonly IAccountAppService _service;
    public AccountController(IAccountAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<AccountDto>> GetListAsync(PagedAndSortedResultRequestDto input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<AccountDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<AccountDto> CreateAsync(CreateUpdateAccountDto input) => _service.CreateAsync(input);
    [HttpPut("{id}")] public Task<AccountDto> UpdateAsync(Guid id, CreateUpdateAccountDto input) => _service.UpdateAsync(id, input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);
    [HttpPost("transfer")] public Task<MoneyTransferDto> TransferAsync(CreateMoneyTransferDto input) => _service.TransferAsync(input);
    [HttpGet("transfers")] public Task<PagedResultDto<MoneyTransferDto>> GetTransfersAsync(PagedAndSortedResultRequestDto input) => _service.GetTransfersAsync(input);
    [HttpPost("deposit")] public Task<DepositDto> DepositAsync(CreateDepositDto input) => _service.DepositAsync(input);
    [HttpGet("deposits")] public Task<PagedResultDto<DepositDto>> GetDepositsAsync(PagedAndSortedResultRequestDto input) => _service.GetDepositsAsync(input);
}

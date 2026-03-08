using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/stock")]
public class StockController : AbpController, IStockAppService
{
    private readonly IStockAppService _service;
    public StockController(IStockAppService service) => _service = service;

    [HttpGet("adjustments")] public Task<PagedResultDto<StockAdjustmentDto>> GetAdjustmentsAsync(PagedAndSortedResultRequestDto input) => _service.GetAdjustmentsAsync(input);
    [HttpPost("adjustments")] public Task<StockAdjustmentDto> CreateAdjustmentAsync(CreateStockAdjustmentDto input) => _service.CreateAdjustmentAsync(input);
    [HttpGet("transfers")] public Task<PagedResultDto<StockTransferDto>> GetTransfersAsync(PagedAndSortedResultRequestDto input) => _service.GetTransfersAsync(input);
    [HttpPost("transfers")] public Task<StockTransferDto> CreateTransferAsync(CreateStockTransferDto input) => _service.CreateTransferAsync(input);
    [HttpGet("ledger")] public Task<PagedResultDto<StockLedgerDto>> GetLedgerAsync([FromQuery] Guid? itemId, PagedAndSortedResultRequestDto input) => _service.GetLedgerAsync(itemId, input);
}

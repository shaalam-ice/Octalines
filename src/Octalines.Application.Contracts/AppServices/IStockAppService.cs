using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IStockAppService : IApplicationService
{
    Task<PagedResultDto<StockAdjustmentDto>> GetAdjustmentsAsync(PagedAndSortedResultRequestDto input);
    Task<StockAdjustmentDto> CreateAdjustmentAsync(CreateStockAdjustmentDto input);
    Task<PagedResultDto<StockTransferDto>> GetTransfersAsync(PagedAndSortedResultRequestDto input);
    Task<StockTransferDto> CreateTransferAsync(CreateStockTransferDto input);
    Task<PagedResultDto<StockLedgerDto>> GetLedgerAsync(Guid? itemId, PagedAndSortedResultRequestDto input);
}

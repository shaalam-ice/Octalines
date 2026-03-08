using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IQuotationAppService : IApplicationService
{
    Task<PagedResultDto<QuotationDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<QuotationDto> GetAsync(Guid id);
    Task<QuotationDto> CreateAsync(CreateQuotationDto input);
    Task DeleteAsync(Guid id);
    Task<SaleDto> ConvertToSaleAsync(Guid quotationId, Guid warehouseId);
}

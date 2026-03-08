using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface ISaleAppService : IApplicationService
{
    Task<PagedResultDto<SaleDto>> GetListAsync(GetSalesInput input);
    Task<SaleDto> GetAsync(Guid id);
    Task<SaleDto> CreateAsync(CreateSaleDto input);
    Task DeleteAsync(Guid id);
    Task<SaleDto> AddPaymentAsync(AddSalePaymentDto input);
    Task<SaleReturnDto> CreateReturnAsync(CreateSaleReturnDto input);
    Task<PagedResultDto<SaleReturnDto>> GetReturnsAsync(PagedAndSortedResultRequestDto input);
    Task<string> GetNextInvoiceNumberAsync();
}

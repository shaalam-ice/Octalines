using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IPurchaseAppService : IApplicationService
{
    Task<PagedResultDto<PurchaseDto>> GetListAsync(GetPurchasesInput input);
    Task<PurchaseDto> GetAsync(Guid id);
    Task<PurchaseDto> CreateAsync(CreatePurchaseDto input);
    Task DeleteAsync(Guid id);
    Task<PurchaseDto> AddPaymentAsync(Guid purchaseId, CreatePurchasePaymentDto input);
    Task<string> GetNextPurchaseNumberAsync();
}

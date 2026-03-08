using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/purchases")]
public class PurchaseController : AbpController, IPurchaseAppService
{
    private readonly IPurchaseAppService _service;
    public PurchaseController(IPurchaseAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<PurchaseDto>> GetListAsync(GetPurchasesInput input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<PurchaseDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<PurchaseDto> CreateAsync(CreatePurchaseDto input) => _service.CreateAsync(input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);
    [HttpPost("{purchaseId}/payment")] public Task<PurchaseDto> AddPaymentAsync(Guid purchaseId, CreatePurchasePaymentDto input) => _service.AddPaymentAsync(purchaseId, input);
    [HttpGet("next-number")] public Task<string> GetNextPurchaseNumberAsync() => _service.GetNextPurchaseNumberAsync();
}

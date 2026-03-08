using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/quotations")]
public class QuotationController : AbpController, IQuotationAppService
{
    private readonly IQuotationAppService _service;
    public QuotationController(IQuotationAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<QuotationDto>> GetListAsync(PagedAndSortedResultRequestDto input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<QuotationDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<QuotationDto> CreateAsync(CreateQuotationDto input) => _service.CreateAsync(input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);
    [HttpPost("{id}/convert-to-sale")] public Task<SaleDto> ConvertToSaleAsync(Guid id, [FromQuery] Guid warehouseId) => _service.ConvertToSaleAsync(id, warehouseId);
}

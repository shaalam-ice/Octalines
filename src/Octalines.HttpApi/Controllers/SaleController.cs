using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/sales")]
public class SaleController : AbpController, ISaleAppService
{
    private readonly ISaleAppService _service;
    public SaleController(ISaleAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<SaleDto>> GetListAsync(GetSalesInput input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<SaleDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<SaleDto> CreateAsync(CreateSaleDto input) => _service.CreateAsync(input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);
    [HttpPost("payment")] public Task<SaleDto> AddPaymentAsync(AddSalePaymentDto input) => _service.AddPaymentAsync(input);
    [HttpPost("returns")] public Task<SaleReturnDto> CreateReturnAsync(CreateSaleReturnDto input) => _service.CreateReturnAsync(input);
    [HttpGet("returns")] public Task<PagedResultDto<SaleReturnDto>> GetReturnsAsync(PagedAndSortedResultRequestDto input) => _service.GetReturnsAsync(input);
    [HttpGet("next-number")] public Task<string> GetNextInvoiceNumberAsync() => _service.GetNextInvoiceNumberAsync();
}

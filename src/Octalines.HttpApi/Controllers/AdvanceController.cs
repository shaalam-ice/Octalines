using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/advances")]
public class AdvanceController : AbpController, IAdvancePaymentAppService
{
    private readonly IAdvancePaymentAppService _service;
    public AdvanceController(IAdvancePaymentAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<AdvancePaymentDto>> GetListAsync(PagedAndSortedResultRequestDto input) => _service.GetListAsync(input);
    [HttpPost] public Task<AdvancePaymentDto> CreateAsync(CreateAdvancePaymentDto input) => _service.CreateAsync(input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);
}

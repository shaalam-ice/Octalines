using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/warehouses")]
public class WarehouseController : AbpController, IWarehouseAppService
{
    private readonly IWarehouseAppService _service;
    public WarehouseController(IWarehouseAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<WarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<WarehouseDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<WarehouseDto> CreateAsync(CreateUpdateWarehouseDto input) => _service.CreateAsync(input);
    [HttpPut("{id}")] public Task<WarehouseDto> UpdateAsync(Guid id, CreateUpdateWarehouseDto input) => _service.UpdateAsync(id, input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);
    [HttpGet("all")] public Task<List<WarehouseDto>> GetAllAsync() => _service.GetAllAsync();
}

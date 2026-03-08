using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/items")]
public class ItemController : AbpController, IItemAppService
{
    private readonly IItemAppService _service;
    public ItemController(IItemAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<ItemDto>> GetListAsync(GetItemsInput input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<ItemDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<ItemDto> CreateAsync(CreateUpdateItemDto input) => _service.CreateAsync(input);
    [HttpPut("{id}")] public Task<ItemDto> UpdateAsync(Guid id, CreateUpdateItemDto input) => _service.UpdateAsync(id, input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);

    Task IItemAppService.ImportFromCsvAsync(Stream csvStream, ItemType itemType)
        => _service.ImportFromCsvAsync(csvStream, itemType);

    [HttpPost("import")] public Task ImportFromCsvAsync(IFormFile file, [FromQuery] ItemType itemType)
        => _service.ImportFromCsvAsync(file.OpenReadStream(), itemType);
    [HttpGet("search")] public Task<List<ItemDto>> SearchAsync([FromQuery] string? filter, [FromQuery] int maxCount = 20)
        => _service.SearchAsync(filter, maxCount);
}

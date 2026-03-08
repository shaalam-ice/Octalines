using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IItemAppService : IApplicationService
{
    Task<PagedResultDto<ItemDto>> GetListAsync(GetItemsInput input);
    Task<ItemDto> GetAsync(Guid id);
    Task<ItemDto> CreateAsync(CreateUpdateItemDto input);
    Task<ItemDto> UpdateAsync(Guid id, CreateUpdateItemDto input);
    Task DeleteAsync(Guid id);
    Task ImportFromCsvAsync(Stream csvStream, ItemType itemType);
    Task<List<ItemDto>> SearchAsync(string? filter, int maxCount = 20);
}

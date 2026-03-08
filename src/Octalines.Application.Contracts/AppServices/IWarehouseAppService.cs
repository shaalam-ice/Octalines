using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IWarehouseAppService : IApplicationService
{
    Task<PagedResultDto<WarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<WarehouseDto> GetAsync(Guid id);
    Task<WarehouseDto> CreateAsync(CreateUpdateWarehouseDto input);
    Task<WarehouseDto> UpdateAsync(Guid id, CreateUpdateWarehouseDto input);
    Task DeleteAsync(Guid id);
    Task<List<WarehouseDto>> GetAllAsync();
}

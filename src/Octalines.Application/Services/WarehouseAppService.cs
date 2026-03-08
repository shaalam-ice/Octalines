using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Warehouses.Default)]
public class WarehouseAppService : ApplicationService, IWarehouseAppService
{
    private readonly IRepository<Warehouse, Guid> _warehouseRepository;

    public WarehouseAppService(IRepository<Warehouse, Guid> warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public async Task<PagedResultDto<WarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _warehouseRepository.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<WarehouseDto>(total, ObjectMapper.Map<List<Warehouse>, List<WarehouseDto>>(items));
    }

    public async Task<WarehouseDto> GetAsync(Guid id)
    {
        var w = await _warehouseRepository.GetAsync(id);
        return ObjectMapper.Map<Warehouse, WarehouseDto>(w);
    }

    [Authorize(OctalinesPermissions.Warehouses.Create)]
    public async Task<WarehouseDto> CreateAsync(CreateUpdateWarehouseDto input)
    {
        var w = new Warehouse(GuidGenerator.Create());
        ObjectMapper.Map(input, w);
        await _warehouseRepository.InsertAsync(w, autoSave: true);
        return ObjectMapper.Map<Warehouse, WarehouseDto>(w);
    }

    [Authorize(OctalinesPermissions.Warehouses.Edit)]
    public async Task<WarehouseDto> UpdateAsync(Guid id, CreateUpdateWarehouseDto input)
    {
        var w = await _warehouseRepository.GetAsync(id);
        ObjectMapper.Map(input, w);
        await _warehouseRepository.UpdateAsync(w, autoSave: true);
        return ObjectMapper.Map<Warehouse, WarehouseDto>(w);
    }

    [Authorize(OctalinesPermissions.Warehouses.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _warehouseRepository.DeleteAsync(id, autoSave: true);
    }

    public async Task<List<WarehouseDto>> GetAllAsync()
    {
        var items = await _warehouseRepository.GetListAsync();
        return ObjectMapper.Map<List<Warehouse>, List<WarehouseDto>>(items);
    }
}

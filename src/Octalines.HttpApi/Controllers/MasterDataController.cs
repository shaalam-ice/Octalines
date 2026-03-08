using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/master-data")]
public class MasterDataController : AbpController, IMasterDataAppService
{
    private readonly IMasterDataAppService _service;
    public MasterDataController(IMasterDataAppService service) => _service = service;

    [HttpGet("categories")] public Task<PagedResultDto<CategoryDto>> GetCategoriesAsync(PagedAndSortedResultRequestDto input) => _service.GetCategoriesAsync(input);
    [HttpPost("categories")] public Task<CategoryDto> CreateCategoryAsync(CreateUpdateCategoryDto input) => _service.CreateCategoryAsync(input);
    [HttpPut("categories/{id}")] public Task<CategoryDto> UpdateCategoryAsync(Guid id, CreateUpdateCategoryDto input) => _service.UpdateCategoryAsync(id, input);
    [HttpDelete("categories/{id}")] public Task DeleteCategoryAsync(Guid id) => _service.DeleteCategoryAsync(id);

    [HttpGet("brands")] public Task<PagedResultDto<BrandDto>> GetBrandsAsync(PagedAndSortedResultRequestDto input) => _service.GetBrandsAsync(input);
    [HttpPost("brands")] public Task<BrandDto> CreateBrandAsync(CreateUpdateBrandDto input) => _service.CreateBrandAsync(input);
    [HttpPut("brands/{id}")] public Task<BrandDto> UpdateBrandAsync(Guid id, CreateUpdateBrandDto input) => _service.UpdateBrandAsync(id, input);
    [HttpDelete("brands/{id}")] public Task DeleteBrandAsync(Guid id) => _service.DeleteBrandAsync(id);

    [HttpGet("units")] public Task<PagedResultDto<UnitDto>> GetUnitsAsync(PagedAndSortedResultRequestDto input) => _service.GetUnitsAsync(input);
    [HttpPost("units")] public Task<UnitDto> CreateUnitAsync(CreateUpdateUnitDto input) => _service.CreateUnitAsync(input);
    [HttpPut("units/{id}")] public Task<UnitDto> UpdateUnitAsync(Guid id, CreateUpdateUnitDto input) => _service.UpdateUnitAsync(id, input);
    [HttpDelete("units/{id}")] public Task DeleteUnitAsync(Guid id) => _service.DeleteUnitAsync(id);

    [HttpGet("taxes")] public Task<PagedResultDto<TaxDto>> GetTaxesAsync(PagedAndSortedResultRequestDto input) => _service.GetTaxesAsync(input);
    [HttpPost("taxes")] public Task<TaxDto> CreateTaxAsync(CreateUpdateTaxDto input) => _service.CreateTaxAsync(input);
    [HttpPut("taxes/{id}")] public Task<TaxDto> UpdateTaxAsync(Guid id, CreateUpdateTaxDto input) => _service.UpdateTaxAsync(id, input);
    [HttpDelete("taxes/{id}")] public Task DeleteTaxAsync(Guid id) => _service.DeleteTaxAsync(id);

    [HttpGet("categories/all")] public Task<List<CategoryDto>> GetAllCategoriesAsync() => _service.GetAllCategoriesAsync();
    [HttpGet("brands/all")] public Task<List<BrandDto>> GetAllBrandsAsync() => _service.GetAllBrandsAsync();
    [HttpGet("units/all")] public Task<List<UnitDto>> GetAllUnitsAsync() => _service.GetAllUnitsAsync();
    [HttpGet("taxes/all")] public Task<List<TaxDto>> GetAllTaxesAsync() => _service.GetAllTaxesAsync();
}

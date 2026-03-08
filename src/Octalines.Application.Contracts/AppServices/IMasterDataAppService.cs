using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IMasterDataAppService : IApplicationService
{
    // Categories
    Task<PagedResultDto<CategoryDto>> GetCategoriesAsync(PagedAndSortedResultRequestDto input);
    Task<CategoryDto> CreateCategoryAsync(CreateUpdateCategoryDto input);
    Task<CategoryDto> UpdateCategoryAsync(Guid id, CreateUpdateCategoryDto input);
    Task DeleteCategoryAsync(Guid id);

    // Brands
    Task<PagedResultDto<BrandDto>> GetBrandsAsync(PagedAndSortedResultRequestDto input);
    Task<BrandDto> CreateBrandAsync(CreateUpdateBrandDto input);
    Task<BrandDto> UpdateBrandAsync(Guid id, CreateUpdateBrandDto input);
    Task DeleteBrandAsync(Guid id);

    // Units
    Task<PagedResultDto<UnitDto>> GetUnitsAsync(PagedAndSortedResultRequestDto input);
    Task<UnitDto> CreateUnitAsync(CreateUpdateUnitDto input);
    Task<UnitDto> UpdateUnitAsync(Guid id, CreateUpdateUnitDto input);
    Task DeleteUnitAsync(Guid id);

    // Taxes
    Task<PagedResultDto<TaxDto>> GetTaxesAsync(PagedAndSortedResultRequestDto input);
    Task<TaxDto> CreateTaxAsync(CreateUpdateTaxDto input);
    Task<TaxDto> UpdateTaxAsync(Guid id, CreateUpdateTaxDto input);
    Task DeleteTaxAsync(Guid id);

    // Lookup lists
    Task<List<CategoryDto>> GetAllCategoriesAsync();
    Task<List<BrandDto>> GetAllBrandsAsync();
    Task<List<UnitDto>> GetAllUnitsAsync();
    Task<List<TaxDto>> GetAllTaxesAsync();
}

using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Items.Default)]
public class MasterDataAppService : ApplicationService, IMasterDataAppService
{
    private readonly IRepository<Category, Guid> _categoryRepo;
    private readonly IRepository<Brand, Guid> _brandRepo;
    private readonly IRepository<Unit, Guid> _unitRepo;
    private readonly IRepository<Tax, Guid> _taxRepo;

    public MasterDataAppService(
        IRepository<Category, Guid> categoryRepo,
        IRepository<Brand, Guid> brandRepo,
        IRepository<Unit, Guid> unitRepo,
        IRepository<Tax, Guid> taxRepo)
    {
        _categoryRepo = categoryRepo;
        _brandRepo = brandRepo;
        _unitRepo = unitRepo;
        _taxRepo = taxRepo;
    }

    public async Task<PagedResultDto<CategoryDto>> GetCategoriesAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _categoryRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<CategoryDto>(total, ObjectMapper.Map<List<Category>, List<CategoryDto>>(items));
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateUpdateCategoryDto input)
    {
        var c = new Category(GuidGenerator.Create());
        ObjectMapper.Map(input, c);
        await _categoryRepo.InsertAsync(c, autoSave: true);
        return ObjectMapper.Map<Category, CategoryDto>(c);
    }

    public async Task<CategoryDto> UpdateCategoryAsync(Guid id, CreateUpdateCategoryDto input)
    {
        var c = await _categoryRepo.GetAsync(id);
        ObjectMapper.Map(input, c);
        await _categoryRepo.UpdateAsync(c, autoSave: true);
        return ObjectMapper.Map<Category, CategoryDto>(c);
    }

    public async Task DeleteCategoryAsync(Guid id) => await _categoryRepo.DeleteAsync(id, autoSave: true);

    public async Task<PagedResultDto<BrandDto>> GetBrandsAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _brandRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<BrandDto>(total, ObjectMapper.Map<List<Brand>, List<BrandDto>>(items));
    }

    public async Task<BrandDto> CreateBrandAsync(CreateUpdateBrandDto input)
    {
        var b = new Brand(GuidGenerator.Create());
        ObjectMapper.Map(input, b);
        await _brandRepo.InsertAsync(b, autoSave: true);
        return ObjectMapper.Map<Brand, BrandDto>(b);
    }

    public async Task<BrandDto> UpdateBrandAsync(Guid id, CreateUpdateBrandDto input)
    {
        var b = await _brandRepo.GetAsync(id);
        ObjectMapper.Map(input, b);
        await _brandRepo.UpdateAsync(b, autoSave: true);
        return ObjectMapper.Map<Brand, BrandDto>(b);
    }

    public async Task DeleteBrandAsync(Guid id) => await _brandRepo.DeleteAsync(id, autoSave: true);

    public async Task<PagedResultDto<UnitDto>> GetUnitsAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _unitRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<UnitDto>(total, ObjectMapper.Map<List<Unit>, List<UnitDto>>(items));
    }

    public async Task<UnitDto> CreateUnitAsync(CreateUpdateUnitDto input)
    {
        var u = new Unit(GuidGenerator.Create());
        ObjectMapper.Map(input, u);
        await _unitRepo.InsertAsync(u, autoSave: true);
        return ObjectMapper.Map<Unit, UnitDto>(u);
    }

    public async Task<UnitDto> UpdateUnitAsync(Guid id, CreateUpdateUnitDto input)
    {
        var u = await _unitRepo.GetAsync(id);
        ObjectMapper.Map(input, u);
        await _unitRepo.UpdateAsync(u, autoSave: true);
        return ObjectMapper.Map<Unit, UnitDto>(u);
    }

    public async Task DeleteUnitAsync(Guid id) => await _unitRepo.DeleteAsync(id, autoSave: true);

    public async Task<PagedResultDto<TaxDto>> GetTaxesAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _taxRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<TaxDto>(total, ObjectMapper.Map<List<Tax>, List<TaxDto>>(items));
    }

    public async Task<TaxDto> CreateTaxAsync(CreateUpdateTaxDto input)
    {
        var t = new Tax(GuidGenerator.Create());
        ObjectMapper.Map(input, t);
        await _taxRepo.InsertAsync(t, autoSave: true);
        return ObjectMapper.Map<Tax, TaxDto>(t);
    }

    public async Task<TaxDto> UpdateTaxAsync(Guid id, CreateUpdateTaxDto input)
    {
        var t = await _taxRepo.GetAsync(id);
        ObjectMapper.Map(input, t);
        await _taxRepo.UpdateAsync(t, autoSave: true);
        return ObjectMapper.Map<Tax, TaxDto>(t);
    }

    public async Task DeleteTaxAsync(Guid id) => await _taxRepo.DeleteAsync(id, autoSave: true);

    public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        => ObjectMapper.Map<List<Category>, List<CategoryDto>>(await _categoryRepo.GetListAsync());

    public async Task<List<BrandDto>> GetAllBrandsAsync()
        => ObjectMapper.Map<List<Brand>, List<BrandDto>>(await _brandRepo.GetListAsync());

    public async Task<List<UnitDto>> GetAllUnitsAsync()
        => ObjectMapper.Map<List<Unit>, List<UnitDto>>(await _unitRepo.GetListAsync());

    public async Task<List<TaxDto>> GetAllTaxesAsync()
        => ObjectMapper.Map<List<Tax>, List<TaxDto>>(await _taxRepo.GetListAsync());
}

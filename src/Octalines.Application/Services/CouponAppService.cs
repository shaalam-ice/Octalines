using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Coupons.Default)]
public class CouponAppService : ApplicationService, ICouponAppService
{
    private readonly IRepository<Coupon, Guid> _couponRepo;
    private readonly IRepository<CustomerCoupon, Guid> _customerCouponRepo;
    private readonly IRepository<Contact, Guid> _contactRepo;

    public CouponAppService(
        IRepository<Coupon, Guid> couponRepo,
        IRepository<CustomerCoupon, Guid> customerCouponRepo,
        IRepository<Contact, Guid> contactRepo)
    {
        _couponRepo = couponRepo;
        _customerCouponRepo = customerCouponRepo;
        _contactRepo = contactRepo;
    }

    public async Task<PagedResultDto<CouponDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _couponRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<CouponDto>(total, ObjectMapper.Map<List<Coupon>, List<CouponDto>>(items));
    }

    public async Task<CouponDto> GetAsync(Guid id)
        => ObjectMapper.Map<Coupon, CouponDto>(await _couponRepo.GetAsync(id));

    [Authorize(OctalinesPermissions.Coupons.Create)]
    public async Task<CouponDto> CreateAsync(CreateUpdateCouponDto input)
    {
        var coupon = new Coupon(GuidGenerator.Create());
        ObjectMapper.Map(input, coupon);
        await _couponRepo.InsertAsync(coupon, autoSave: true);
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    [Authorize(OctalinesPermissions.Coupons.Edit)]
    public async Task<CouponDto> UpdateAsync(Guid id, CreateUpdateCouponDto input)
    {
        var coupon = await _couponRepo.GetAsync(id);
        ObjectMapper.Map(input, coupon);
        await _couponRepo.UpdateAsync(coupon, autoSave: true);
        return ObjectMapper.Map<Coupon, CouponDto>(coupon);
    }

    [Authorize(OctalinesPermissions.Coupons.Delete)]
    public async Task DeleteAsync(Guid id)
        => await _couponRepo.DeleteAsync(id, autoSave: true);

    public async Task<PagedResultDto<CustomerCouponDto>> GetCustomerCouponsAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _customerCouponRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        var contacts = await _contactRepo.GetListAsync();
        var dtos = items.Select(cc =>
        {
            var dto = ObjectMapper.Map<CustomerCoupon, CustomerCouponDto>(cc);
            dto.CustomerName = contacts.FirstOrDefault(c => c.Id == cc.CustomerId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<CustomerCouponDto>(total, dtos);
    }

    [Authorize(OctalinesPermissions.Coupons.Create)]
    public async Task<CustomerCouponDto> CreateCustomerCouponAsync(CreateCustomerCouponDto input)
    {
        var cc = new CustomerCoupon(GuidGenerator.Create())
            {
            CustomerId = input.CustomerId,
            Code = input.Code,
            DiscountAmount = input.DiscountAmount,
            ExpiryDate = input.ExpiryDate,
            IsUsed = false
        };
        await _customerCouponRepo.InsertAsync(cc, autoSave: true);
        var dto = ObjectMapper.Map<CustomerCoupon, CustomerCouponDto>(cc);
        dto.CustomerName = (await _contactRepo.FindAsync(input.CustomerId))?.Name;
        return dto;
    }

    [Authorize(OctalinesPermissions.Coupons.Delete)]
    public async Task DeleteCustomerCouponAsync(Guid id)
        => await _customerCouponRepo.DeleteAsync(id, autoSave: true);
}

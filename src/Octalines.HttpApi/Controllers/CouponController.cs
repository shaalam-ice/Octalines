using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/coupons")]
public class CouponController : AbpController, ICouponAppService
{
    private readonly ICouponAppService _service;
    public CouponController(ICouponAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<CouponDto>> GetListAsync(PagedAndSortedResultRequestDto input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<CouponDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<CouponDto> CreateAsync(CreateUpdateCouponDto input) => _service.CreateAsync(input);
    [HttpPut("{id}")] public Task<CouponDto> UpdateAsync(Guid id, CreateUpdateCouponDto input) => _service.UpdateAsync(id, input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);
    [HttpGet("customer-coupons")] public Task<PagedResultDto<CustomerCouponDto>> GetCustomerCouponsAsync(PagedAndSortedResultRequestDto input) => _service.GetCustomerCouponsAsync(input);
    [HttpPost("customer-coupons")] public Task<CustomerCouponDto> CreateCustomerCouponAsync(CreateCustomerCouponDto input) => _service.CreateCustomerCouponAsync(input);
    [HttpDelete("customer-coupons/{id}")] public Task DeleteCustomerCouponAsync(Guid id) => _service.DeleteCustomerCouponAsync(id);
}

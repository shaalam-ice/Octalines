using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface ICouponAppService : IApplicationService
{
    Task<PagedResultDto<CouponDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<CouponDto> GetAsync(Guid id);
    Task<CouponDto> CreateAsync(CreateUpdateCouponDto input);
    Task<CouponDto> UpdateAsync(Guid id, CreateUpdateCouponDto input);
    Task DeleteAsync(Guid id);
    Task<PagedResultDto<CustomerCouponDto>> GetCustomerCouponsAsync(PagedAndSortedResultRequestDto input);
    Task<CustomerCouponDto> CreateCustomerCouponAsync(CreateCustomerCouponDto input);
    Task DeleteCustomerCouponAsync(Guid id);
}

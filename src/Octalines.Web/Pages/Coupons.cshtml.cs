using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class CouponsModel : PageModel
{
    private readonly ICouponAppService _service;
    public PagedResultDto<CouponDto>? Coupons { get; set; }

    public CouponsModel(ICouponAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Coupons = await _service.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); }
        catch { Coupons = new PagedResultDto<CouponDto>(); }
    }
}

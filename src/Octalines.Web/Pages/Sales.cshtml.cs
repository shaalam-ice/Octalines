using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class SalesModel : PageModel
{
    private readonly ISaleAppService _service;
    [BindProperty(SupportsGet = true)] public string? Filter { get; set; }
    public PagedResultDto<SaleDto>? Sales { get; set; }

    public SalesModel(ISaleAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Sales = await _service.GetListAsync(new GetSalesInput { Filter = Filter, MaxResultCount = 50 }); }
        catch { Sales = new PagedResultDto<SaleDto>(); }
    }
}

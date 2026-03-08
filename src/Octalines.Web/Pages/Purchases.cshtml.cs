using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class PurchasesModel : PageModel
{
    private readonly IPurchaseAppService _service;
    [BindProperty(SupportsGet = true)] public string? Filter { get; set; }
    public PagedResultDto<PurchaseDto>? Purchases { get; set; }

    public PurchasesModel(IPurchaseAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Purchases = await _service.GetListAsync(new GetPurchasesInput { Filter = Filter, MaxResultCount = 50 }); }
        catch { Purchases = new PagedResultDto<PurchaseDto>(); }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages.Stock;

public class AdjustmentsModel : PageModel
{
    private readonly IStockAppService _service;
    public PagedResultDto<StockAdjustmentDto>? Adjustments { get; set; }

    public AdjustmentsModel(IStockAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Adjustments = await _service.GetAdjustmentsAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); }
        catch { Adjustments = new PagedResultDto<StockAdjustmentDto>(); }
    }
}

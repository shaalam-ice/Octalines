using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages.Stock;

public class TransfersModel : PageModel
{
    private readonly IStockAppService _service;
    public PagedResultDto<StockTransferDto>? Transfers { get; set; }

    public TransfersModel(IStockAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Transfers = await _service.GetTransfersAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); }
        catch { Transfers = new PagedResultDto<StockTransferDto>(); }
    }
}

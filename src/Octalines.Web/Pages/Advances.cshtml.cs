using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class AdvancesModel : PageModel
{
    private readonly IAdvancePaymentAppService _service;
    public PagedResultDto<AdvancePaymentDto>? Advances { get; set; }

    public AdvancesModel(IAdvancePaymentAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Advances = await _service.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); }
        catch { Advances = new PagedResultDto<AdvancePaymentDto>(); }
    }
}

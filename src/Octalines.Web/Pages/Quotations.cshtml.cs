using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class QuotationsModel : PageModel
{
    private readonly IQuotationAppService _service;
    public PagedResultDto<QuotationDto>? Quotations { get; set; }

    public QuotationsModel(IQuotationAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Quotations = await _service.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); }
        catch { Quotations = new PagedResultDto<QuotationDto>(); }
    }
}

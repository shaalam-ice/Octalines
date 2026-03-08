using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages.Sales;

public class DetailModel : PageModel
{
    private readonly ISaleAppService _service;
    public SaleDto? Sale { get; set; }

    public DetailModel(ISaleAppService service) => _service = service;

    public async Task OnGetAsync(Guid id)
    {
        try { Sale = await _service.GetAsync(id); } catch { }
    }
}

using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class WarehousesModel : PageModel
{
    private readonly IWarehouseAppService _service;
    public PagedResultDto<WarehouseDto>? Warehouses { get; set; }

    public WarehousesModel(IWarehouseAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Warehouses = await _service.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); }
        catch { Warehouses = new PagedResultDto<WarehouseDto>(); }
    }
}

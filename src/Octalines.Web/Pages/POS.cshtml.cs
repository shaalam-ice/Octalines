using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages;

public class POSModel : PageModel
{
    private readonly IContactAppService _contactService;
    private readonly IWarehouseAppService _warehouseService;
    public List<ContactDto> Customers { get; set; } = new();
    public List<WarehouseDto> Warehouses { get; set; } = new();

    public POSModel(IContactAppService contactService, IWarehouseAppService warehouseService)
    {
        _contactService = contactService;
        _warehouseService = warehouseService;
    }

    public async Task OnGetAsync()
    {
        try
        {
            var r = await _contactService.GetListAsync(new GetContactsInput { ContactType = ContactType.Customer, MaxResultCount = 200 });
            Customers = r.Items.ToList();
        }
        catch { }
        try { Warehouses = await _warehouseService.GetAllAsync(); } catch { }
    }
}

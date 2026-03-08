using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class ItemsModel : PageModel
{
    private readonly IItemAppService _service;
    [BindProperty(SupportsGet = true)] public string? Filter { get; set; }
    public PagedResultDto<ItemDto>? Items { get; set; }

    public ItemsModel(IItemAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Items = await _service.GetListAsync(new GetItemsInput { Filter = Filter, MaxResultCount = 50 }); }
        catch { Items = new PagedResultDto<ItemDto>(); }
    }
}

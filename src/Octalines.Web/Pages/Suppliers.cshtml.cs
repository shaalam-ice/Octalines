using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class SuppliersModel : PageModel
{
    private readonly IContactAppService _service;
    [BindProperty(SupportsGet = true)] public string? Filter { get; set; }
    public PagedResultDto<ContactDto>? Suppliers { get; set; }

    public SuppliersModel(IContactAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try
        {
            Suppliers = await _service.GetListAsync(new GetContactsInput
            {
                ContactType = ContactType.Supplier,
                Filter = Filter,
                MaxResultCount = 50
            });
        }
        catch { Suppliers = new PagedResultDto<ContactDto>(); }
    }
}

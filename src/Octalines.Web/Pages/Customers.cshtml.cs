using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class CustomersModel : PageModel
{
    private readonly IContactAppService _service;
    [BindProperty(SupportsGet = true)] public string? Filter { get; set; }
    [BindProperty(SupportsGet = true)] public int CurrentPage { get; set; } = 1;
    public PagedResultDto<ContactDto>? Customers { get; set; }

    public CustomersModel(IContactAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try
        {
            Customers = await _service.GetListAsync(new GetContactsInput
            {
                ContactType = ContactType.Customer,
                Filter = Filter,
                SkipCount = (CurrentPage - 1) * 20,
                MaxResultCount = 20
            });
        }
        catch { Customers = new PagedResultDto<ContactDto>(); }
    }
}

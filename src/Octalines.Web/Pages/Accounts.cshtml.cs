using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class AccountsModel : PageModel
{
    private readonly IAccountAppService _service;
    public PagedResultDto<AccountDto>? Accounts { get; set; }

    public AccountsModel(IAccountAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Accounts = await _service.GetListAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); }
        catch { Accounts = new PagedResultDto<AccountDto>(); }
    }
}

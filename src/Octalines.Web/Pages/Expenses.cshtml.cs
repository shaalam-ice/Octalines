using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class ExpensesModel : PageModel
{
    private readonly IExpenseAppService _service;
    public PagedResultDto<ExpenseDto>? Expenses { get; set; }

    public ExpensesModel(IExpenseAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Expenses = await _service.GetListAsync(new GetExpensesInput { MaxResultCount = 50 }); }
        catch { Expenses = new PagedResultDto<ExpenseDto>(); }
    }
}

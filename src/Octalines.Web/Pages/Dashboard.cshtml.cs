using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages;

public class DashboardModel : PageModel
{
    private readonly IDashboardAppService _dashboardService;
    public DashboardSummaryDto? Summary { get; set; }

    public DashboardModel(IDashboardAppService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    public async Task OnGetAsync()
    {
        try { Summary = await _dashboardService.GetSummaryAsync(); }
        catch { Summary = new DashboardSummaryDto(); }
    }
}

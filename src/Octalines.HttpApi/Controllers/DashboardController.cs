using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/dashboard")]
public class DashboardController : AbpController, IDashboardAppService
{
    private readonly IDashboardAppService _service;
    public DashboardController(IDashboardAppService service) => _service = service;

    [HttpGet("summary")] public Task<DashboardSummaryDto> GetSummaryAsync() => _service.GetSummaryAsync();
}

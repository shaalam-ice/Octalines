using Octalines.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IDashboardAppService : IApplicationService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
}

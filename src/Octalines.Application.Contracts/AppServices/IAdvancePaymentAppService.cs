using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IAdvancePaymentAppService : IApplicationService
{
    Task<PagedResultDto<AdvancePaymentDto>> GetListAsync(PagedAndSortedResultRequestDto input);
    Task<AdvancePaymentDto> CreateAsync(CreateAdvancePaymentDto input);
    Task DeleteAsync(Guid id);
}

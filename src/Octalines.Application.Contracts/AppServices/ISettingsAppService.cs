using Octalines.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface ISettingsAppService : IApplicationService
{
    Task<StoreSettingsDto> GetAsync();
    Task UpdateAsync(UpdateStoreSettingsDto input);
}

using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/settings")]
public class SettingsController : AbpController, ISettingsAppService
{
    private readonly ISettingsAppService _service;
    public SettingsController(ISettingsAppService service) => _service = service;

    [HttpGet] public Task<StoreSettingsDto> GetAsync() => _service.GetAsync();
    [HttpPut] public Task UpdateAsync(UpdateStoreSettingsDto input) => _service.UpdateAsync(input);
}

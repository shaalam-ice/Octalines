using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Settings.Default)]
public class SettingsAppService : ApplicationService, ISettingsAppService
{
    private readonly IRepository<StoreSettings, Guid> _settingsRepo;

    public SettingsAppService(IRepository<StoreSettings, Guid> settingsRepo)
    {
        _settingsRepo = settingsRepo;
    }

    public async Task<StoreSettingsDto> GetAsync()
    {
        var settings = (await _settingsRepo.GetListAsync()).FirstOrDefault();
        if (settings == null) return new StoreSettingsDto();
        return new StoreSettingsDto
        {
            StoreName = settings.StoreName,
            Address = settings.Address,
            Phone = settings.Phone,
            Currency = settings.Currency,
            AllowNegativeStock = settings.AllowNegativeStock,
            SmsProvider = settings.SmsProvider,
            SmsApiKey = settings.SmsApiKey,
            WhatsAppProvider = settings.WhatsAppProvider,
            WhatsAppApiKey = settings.WhatsAppApiKey
        };
    }

    public async Task UpdateAsync(UpdateStoreSettingsDto input)
    {
        var settings = (await _settingsRepo.GetListAsync()).FirstOrDefault();
        if (settings == null)
        {
            settings = new StoreSettings(GuidGenerator.Create());
            ApplySettings(settings, input);
            await _settingsRepo.InsertAsync(settings, autoSave: true);
        }
        else
        {
            ApplySettings(settings, input);
            await _settingsRepo.UpdateAsync(settings, autoSave: true);
        }
    }

    private static void ApplySettings(StoreSettings settings, UpdateStoreSettingsDto input)
    {
        settings.StoreName = input.StoreName;
        settings.Address = input.Address;
        settings.Phone = input.Phone;
        settings.Currency = input.Currency;
        settings.AllowNegativeStock = input.AllowNegativeStock;
        settings.SmsProvider = input.SmsProvider;
        settings.SmsApiKey = input.SmsApiKey;
        settings.WhatsAppProvider = input.WhatsAppProvider;
        settings.WhatsAppApiKey = input.WhatsAppApiKey;
    }
}

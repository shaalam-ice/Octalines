using Volo.Abp.Domain.Entities.Auditing;

namespace Octalines.Entities;

public class StoreSettings : FullAuditedAggregateRoot<Guid>
{
    public StoreSettings(Guid id) : base(id) { }
    protected StoreSettings() { }

    public string StoreName { get; set; } = "My Store";
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string Currency { get; set; } = "USD";
    public string? LogoUrl { get; set; }
    public bool AllowNegativeStock { get; set; } = false;
    public string? SmsProvider { get; set; }
    public string? SmsApiKey { get; set; }
    public string? WhatsAppProvider { get; set; }
    public string? WhatsAppApiKey { get; set; }
}

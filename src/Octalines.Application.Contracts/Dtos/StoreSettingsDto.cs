using System.ComponentModel.DataAnnotations;

namespace Octalines.Dtos;

public class StoreSettingsDto
{
    public string StoreName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string Currency { get; set; } = "USD";
    public bool AllowNegativeStock { get; set; }
    public string? SmsProvider { get; set; }
    public string? SmsApiKey { get; set; }
    public string? WhatsAppProvider { get; set; }
    public string? WhatsAppApiKey { get; set; }
}

public class UpdateStoreSettingsDto
{
    [Required] public string StoreName { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
    [Required] public string Currency { get; set; } = "USD";
    public bool AllowNegativeStock { get; set; }
    public string? SmsProvider { get; set; }
    public string? SmsApiKey { get; set; }
    public string? WhatsAppProvider { get; set; }
    public string? WhatsAppApiKey { get; set; }
}

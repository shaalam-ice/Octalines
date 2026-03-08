using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages;

public class SettingsPageModel : PageModel
{
    private readonly ISettingsAppService _service;
    [BindProperty] public UpdateStoreSettingsDto Input { get; set; } = new();
    public bool Saved { get; set; }

    public SettingsPageModel(ISettingsAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try
        {
            var s = await _service.GetAsync();
            Input = new UpdateStoreSettingsDto
            {
                StoreName = s.StoreName ?? "",
                Address = s.Address,
                Phone = s.Phone,
                Currency = s.Currency ?? "USD",
                AllowNegativeStock = s.AllowNegativeStock,
                SmsProvider = s.SmsProvider
            };
        }
        catch { Input = new UpdateStoreSettingsDto { StoreName = "Octalines Store", Currency = "USD" }; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        await _service.UpdateAsync(Input);
        Saved = true;
        return Page();
    }
}

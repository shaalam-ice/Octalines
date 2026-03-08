using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp.Application.Dtos;

namespace Octalines.Web.Pages;

public class MessagingModel : PageModel
{
    private readonly IMessagingAppService _service;
    [BindProperty] public SendMessageDto Input { get; set; } = new();
    public PagedResultDto<MessageTemplateDto>? Templates { get; set; }
    public bool Sent { get; set; }

    public MessagingModel(IMessagingAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        try { Templates = await _service.GetTemplatesAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); }
        catch { Templates = new PagedResultDto<MessageTemplateDto>(); }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            try { Templates = await _service.GetTemplatesAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); } catch { }
            return Page();
        }
        try { await _service.SendMessageAsync(Input); Sent = true; }
        catch { ModelState.AddModelError("", "Failed to send message"); }
        try { Templates = await _service.GetTemplatesAsync(new PagedAndSortedResultRequestDto { MaxResultCount = 50 }); } catch { }
        return Page();
    }
}

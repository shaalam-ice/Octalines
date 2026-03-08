using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/messaging")]
public class MessagingController : AbpController, IMessagingAppService
{
    private readonly IMessagingAppService _service;
    public MessagingController(IMessagingAppService service) => _service = service;

    [HttpPost("send")] public Task SendMessageAsync(SendMessageDto input) => _service.SendMessageAsync(input);
    [HttpGet("templates")] public Task<PagedResultDto<MessageTemplateDto>> GetTemplatesAsync(PagedAndSortedResultRequestDto input) => _service.GetTemplatesAsync(input);
    [HttpPost("templates")] public Task<MessageTemplateDto> CreateTemplateAsync(CreateUpdateMessageTemplateDto input) => _service.CreateTemplateAsync(input);
    [HttpPut("templates/{id}")] public Task<MessageTemplateDto> UpdateTemplateAsync(Guid id, CreateUpdateMessageTemplateDto input) => _service.UpdateTemplateAsync(id, input);
    [HttpDelete("templates/{id}")] public Task DeleteTemplateAsync(Guid id) => _service.DeleteTemplateAsync(id);
}

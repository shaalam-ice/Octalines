using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Messaging.Default)]
public class MessagingAppService : ApplicationService, IMessagingAppService
{
    private readonly IRepository<MessageTemplate, Guid> _templateRepo;
    private readonly IRepository<MessageLog, Guid> _logRepo;

    public MessagingAppService(
        IRepository<MessageTemplate, Guid> templateRepo,
        IRepository<MessageLog, Guid> logRepo)
    {
        _templateRepo = templateRepo;
        _logRepo = logRepo;
    }

    [Authorize(OctalinesPermissions.Messaging.Send)]
    public async Task SendMessageAsync(SendMessageDto input)
    {
        // Log the message (mock provider)
        var log = new MessageLog(GuidGenerator.Create())
            {
            RecipientPhone = input.Phone,
            Content = input.Message,
            Channel = input.Channel,
            IsSuccess = true,
            SentAt = DateTime.UtcNow
        };
        await _logRepo.InsertAsync(log, autoSave: true);
    }

    public async Task<PagedResultDto<MessageTemplateDto>> GetTemplatesAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _templateRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<MessageTemplateDto>(total, ObjectMapper.Map<List<MessageTemplate>, List<MessageTemplateDto>>(items));
    }

    public async Task<MessageTemplateDto> CreateTemplateAsync(CreateUpdateMessageTemplateDto input)
    {
        var template = new MessageTemplate(GuidGenerator.Create());
        ObjectMapper.Map(input, template);
        await _templateRepo.InsertAsync(template, autoSave: true);
        return ObjectMapper.Map<MessageTemplate, MessageTemplateDto>(template);
    }

    public async Task<MessageTemplateDto> UpdateTemplateAsync(Guid id, CreateUpdateMessageTemplateDto input)
    {
        var template = await _templateRepo.GetAsync(id);
        ObjectMapper.Map(input, template);
        await _templateRepo.UpdateAsync(template, autoSave: true);
        return ObjectMapper.Map<MessageTemplate, MessageTemplateDto>(template);
    }

    public async Task DeleteTemplateAsync(Guid id)
        => await _templateRepo.DeleteAsync(id, autoSave: true);
}

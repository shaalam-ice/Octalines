using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IMessagingAppService : IApplicationService
{
    Task SendMessageAsync(SendMessageDto input);
    Task<PagedResultDto<MessageTemplateDto>> GetTemplatesAsync(PagedAndSortedResultRequestDto input);
    Task<MessageTemplateDto> CreateTemplateAsync(CreateUpdateMessageTemplateDto input);
    Task<MessageTemplateDto> UpdateTemplateAsync(Guid id, CreateUpdateMessageTemplateDto input);
    Task DeleteTemplateAsync(Guid id);
}

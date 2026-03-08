using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class MessageTemplateDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public MessageChannel Channel { get; set; }
    public bool IsActive { get; set; }
}

public class CreateUpdateMessageTemplateDto
{
    [Required] public string Name { get; set; } = string.Empty;
    [Required] public string Content { get; set; } = string.Empty;
    public MessageChannel Channel { get; set; }
    public bool IsActive { get; set; } = true;
}

public class SendMessageDto
{
    [Required] public string Phone { get; set; } = string.Empty;
    [Required] public string Message { get; set; } = string.Empty;
    public MessageChannel Channel { get; set; } = MessageChannel.SMS;
}

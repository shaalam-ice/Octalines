using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class UnitDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
}

public class CreateUpdateUnitDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? ShortName { get; set; }
}

using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class BrandDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateUpdateBrandDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

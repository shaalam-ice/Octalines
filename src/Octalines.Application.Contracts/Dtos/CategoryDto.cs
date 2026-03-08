using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class CategoryDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class CreateUpdateCategoryDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

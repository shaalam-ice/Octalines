using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class WarehouseDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public bool IsDefault { get; set; }
}

public class CreateUpdateWarehouseDto
{
    [Required] public string Name { get; set; } = string.Empty;
    public string? Location { get; set; }
    public bool IsDefault { get; set; }
}

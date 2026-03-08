using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class TaxDto : FullAuditedEntityDto<Guid>
{
    public string Name { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public bool IsActive { get; set; }
}

public class CreateUpdateTaxDto
{
    [Required] public string Name { get; set; } = string.Empty;
    [Range(0, 100)] public decimal Rate { get; set; }
    public bool IsActive { get; set; } = true;
}

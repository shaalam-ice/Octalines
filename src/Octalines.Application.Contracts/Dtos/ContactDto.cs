using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Octalines.Dtos;

public class ContactDto : FullAuditedEntityDto<Guid>
{
    public ContactType ContactType { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxNumber { get; set; }
    public decimal Balance { get; set; }
    public bool IsActive { get; set; }
}

public class CreateUpdateContactDto
{
    public ContactType ContactType { get; set; }
    [Required] public string Name { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? TaxNumber { get; set; }
    public bool IsActive { get; set; } = true;
}

public class GetContactsInput : PagedAndSortedResultRequestDto
{
    public ContactType? ContactType { get; set; }
    public string? Filter { get; set; }
    public bool? IsActive { get; set; }
}

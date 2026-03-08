using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IContactAppService : IApplicationService
{
    Task<PagedResultDto<ContactDto>> GetListAsync(GetContactsInput input);
    Task<ContactDto> GetAsync(Guid id);
    Task<ContactDto> CreateAsync(CreateUpdateContactDto input);
    Task<ContactDto> UpdateAsync(Guid id, CreateUpdateContactDto input);
    Task DeleteAsync(Guid id);
    Task ImportFromCsvAsync(Stream csvStream, ContactType contactType);
}

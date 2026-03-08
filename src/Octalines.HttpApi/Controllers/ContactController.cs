using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/contacts")]
public class ContactController : AbpController, IContactAppService
{
    private readonly IContactAppService _service;
    public ContactController(IContactAppService contactAppService) => _service = contactAppService;

    [HttpGet] public Task<PagedResultDto<ContactDto>> GetListAsync(GetContactsInput input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<ContactDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<ContactDto> CreateAsync(CreateUpdateContactDto input) => _service.CreateAsync(input);
    [HttpPut("{id}")] public Task<ContactDto> UpdateAsync(Guid id, CreateUpdateContactDto input) => _service.UpdateAsync(id, input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);

    Task IContactAppService.ImportFromCsvAsync(Stream csvStream, ContactType contactType)
        => _service.ImportFromCsvAsync(csvStream, contactType);

    [HttpPost("import")] public Task ImportFromCsvAsync(IFormFile file, [FromQuery] ContactType contactType)
        => _service.ImportFromCsvAsync(file.OpenReadStream(), contactType);
}

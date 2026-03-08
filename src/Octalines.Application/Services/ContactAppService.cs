using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Customers.Default)]
public class ContactAppService : ApplicationService, IContactAppService
{
    private readonly IRepository<Contact, Guid> _contactRepository;

    public ContactAppService(IRepository<Contact, Guid> contactRepository)
    {
        _contactRepository = contactRepository;
    }

    public async Task<PagedResultDto<ContactDto>> GetListAsync(GetContactsInput input)
    {
        var query = await _contactRepository.GetQueryableAsync();

        if (input.ContactType.HasValue)
            query = query.Where(c => c.ContactType == input.ContactType.Value);
        if (!string.IsNullOrWhiteSpace(input.Filter))
            query = query.Where(c => c.Name.Contains(input.Filter) || (c.Phone != null && c.Phone.Contains(input.Filter)));
        if (input.IsActive.HasValue)
            query = query.Where(c => c.IsActive == input.IsActive.Value);

        var total = query.Count();
        var items = query
            .OrderBy(c => c.Name)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToList();

        return new PagedResultDto<ContactDto>(total, ObjectMapper.Map<List<Contact>, List<ContactDto>>(items));
    }

    public async Task<ContactDto> GetAsync(Guid id)
    {
        var contact = await _contactRepository.GetAsync(id);
        return ObjectMapper.Map<Contact, ContactDto>(contact);
    }

    [Authorize(OctalinesPermissions.Customers.Create)]
    public async Task<ContactDto> CreateAsync(CreateUpdateContactDto input)
    {
        var contact = new Contact(GuidGenerator.Create());
        ObjectMapper.Map(input, contact);
        await _contactRepository.InsertAsync(contact, autoSave: true);
        return ObjectMapper.Map<Contact, ContactDto>(contact);
    }

    [Authorize(OctalinesPermissions.Customers.Edit)]
    public async Task<ContactDto> UpdateAsync(Guid id, CreateUpdateContactDto input)
    {
        var contact = await _contactRepository.GetAsync(id);
        ObjectMapper.Map(input, contact);
        await _contactRepository.UpdateAsync(contact, autoSave: true);
        return ObjectMapper.Map<Contact, ContactDto>(contact);
    }

    [Authorize(OctalinesPermissions.Customers.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _contactRepository.DeleteAsync(id, autoSave: true);
    }

    public async Task ImportFromCsvAsync(Stream csvStream, ContactType contactType)
    {
        using var reader = new StreamReader(csvStream);
        await reader.ReadLineAsync(); // skip header
        string? line;
        var contacts = new List<Contact>();
        while ((line = await reader.ReadLineAsync()) != null)
        {
            var parts = line.Split(',');
            if (parts.Length < 1) continue;
            contacts.Add(new Contact(GuidGenerator.Create())
                {
                ContactType = contactType,
                Name = parts[0].Trim(),
                Phone = parts.Length > 1 ? parts[1].Trim() : null,
                Email = parts.Length > 2 ? parts[2].Trim() : null,
                Address = parts.Length > 3 ? parts[3].Trim() : null,
                IsActive = true
            });
        }
        await _contactRepository.InsertManyAsync(contacts, autoSave: true);
    }
}

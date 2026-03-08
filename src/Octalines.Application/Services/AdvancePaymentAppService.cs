using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

public class AdvancePaymentAppService : ApplicationService, IAdvancePaymentAppService
{
    private readonly IRepository<AdvancePayment, Guid> _repo;
    private readonly IRepository<Contact, Guid> _contactRepo;

    public AdvancePaymentAppService(
        IRepository<AdvancePayment, Guid> repo,
        IRepository<Contact, Guid> contactRepo)
    {
        _repo = repo;
        _contactRepo = contactRepo;
    }

    public async Task<PagedResultDto<AdvancePaymentDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _repo.GetQueryableAsync();
        var total = query.Count();
        var items = query.OrderByDescending(a => a.AdvanceDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        var contacts = await _contactRepo.GetListAsync();
        var dtos = items.Select(a =>
        {
            var dto = ObjectMapper.Map<AdvancePayment, AdvancePaymentDto>(a);
            dto.ContactName = contacts.FirstOrDefault(c => c.Id == a.ContactId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<AdvancePaymentDto>(total, dtos);
    }

    public async Task<AdvancePaymentDto> CreateAsync(CreateAdvancePaymentDto input)
    {
        var advance = new AdvancePayment(GuidGenerator.Create())
            {
            ContactId = input.ContactId,
            AdvanceType = input.AdvanceType,
            Amount = input.Amount,
            RemainingAmount = input.Amount,
            AdvanceDate = DateTime.UtcNow,
            Notes = input.Notes
        };
        await _repo.InsertAsync(advance, autoSave: true);
        var dto = ObjectMapper.Map<AdvancePayment, AdvancePaymentDto>(advance);
        dto.ContactName = (await _contactRepo.FindAsync(input.ContactId))?.Name;
        return dto;
    }

    public async Task DeleteAsync(Guid id)
        => await _repo.DeleteAsync(id, autoSave: true);
}

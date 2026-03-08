using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Quotations.Default)]
public class QuotationAppService : ApplicationService, IQuotationAppService
{
    private readonly IRepository<Quotation, Guid> _quotationRepo;
    private readonly IRepository<Item, Guid> _itemRepo;
    private readonly IRepository<Contact, Guid> _contactRepo;
    private readonly ISaleAppService _saleService;

    public QuotationAppService(
        IRepository<Quotation, Guid> quotationRepo,
        IRepository<Item, Guid> itemRepo,
        IRepository<Contact, Guid> contactRepo,
        ISaleAppService saleService)
    {
        _quotationRepo = quotationRepo;
        _itemRepo = itemRepo;
        _contactRepo = contactRepo;
        _saleService = saleService;
    }

    public async Task<PagedResultDto<QuotationDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _quotationRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.OrderByDescending(q => q.QuotationDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        var contacts = await _contactRepo.GetListAsync();
        var dtos = items.Select(q =>
        {
            var dto = ObjectMapper.Map<Quotation, QuotationDto>(q);
            dto.CustomerName = contacts.FirstOrDefault(c => c.Id == q.CustomerId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<QuotationDto>(total, dtos);
    }

    public async Task<QuotationDto> GetAsync(Guid id)
    {
        var q = await _quotationRepo.GetAsync(id);
        var dto = ObjectMapper.Map<Quotation, QuotationDto>(q);
        if (q.CustomerId.HasValue)
            dto.CustomerName = (await _contactRepo.FindAsync(q.CustomerId.Value))?.Name;
        return dto;
    }

    [Authorize(OctalinesPermissions.Quotations.Create)]
    public async Task<QuotationDto> CreateAsync(CreateQuotationDto input)
    {
        var quotation = new Quotation(GuidGenerator.Create())
            {
            QuotationNumber = "QT-" + DateTime.UtcNow.Ticks.ToString()[^6..],
            QuotationDate = input.QuotationDate,
            CustomerId = input.CustomerId,
            DiscountAmount = input.DiscountAmount,
            Notes = input.Notes
        };

        decimal subTotal = 0;
        foreach (var lineInput in input.Lines)
        {
            var item = await _itemRepo.GetAsync(lineInput.ItemId);
            var lineTotal = (lineInput.Quantity * lineInput.UnitPrice) - lineInput.DiscountAmount;
            subTotal += lineTotal;
            quotation.Lines.Add(new QuotationLine(GuidGenerator.Create())
                {
                QuotationId = quotation.Id,
                ItemId = item.Id,
                ItemName = item.Name,
                Quantity = lineInput.Quantity,
                UnitPrice = lineInput.UnitPrice,
                DiscountAmount = lineInput.DiscountAmount,
                LineTotal = lineTotal
            });
        }

        quotation.SubTotal = subTotal;
        quotation.GrandTotal = subTotal - input.DiscountAmount;
        await _quotationRepo.InsertAsync(quotation, autoSave: true);
        return await GetAsync(quotation.Id);
    }

    [Authorize(OctalinesPermissions.Quotations.Delete)]
    public async Task DeleteAsync(Guid id)
        => await _quotationRepo.DeleteAsync(id, autoSave: true);

    public async Task<SaleDto> ConvertToSaleAsync(Guid quotationId, Guid warehouseId)
    {
        var quotation = await _quotationRepo.GetAsync(quotationId);
        var saleDto = await _saleService.CreateAsync(new CreateSaleDto
        {
            CustomerId = quotation.CustomerId,
            WarehouseId = warehouseId,
            DiscountAmount = quotation.DiscountAmount,
            Notes = quotation.Notes,
            Lines = quotation.Lines.Select(l => new CreateSaleLineDto
            {
                ItemId = l.ItemId,
                Quantity = l.Quantity,
                UnitPrice = l.UnitPrice,
                DiscountAmount = l.DiscountAmount
            }).ToList()
        });

        quotation.IsConvertedToSale = true;
        quotation.SaleId = saleDto.Id;
        await _quotationRepo.UpdateAsync(quotation, autoSave: true);
        return saleDto;
    }
}

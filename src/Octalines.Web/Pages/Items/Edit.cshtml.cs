using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages.Items;

public class EditItemModel : PageModel
{
    private readonly IItemAppService _itemService;
    private readonly IMasterDataAppService _masterService;
    [BindProperty(SupportsGet = true)] public Guid Id { get; set; }
    [BindProperty] public CreateUpdateItemDto Input { get; set; } = new();
    public List<CategoryDto> Categories { get; set; } = new();

    public EditItemModel(IItemAppService itemService, IMasterDataAppService masterService)
    {
        _itemService = itemService;
        _masterService = masterService;
    }

    public async Task OnGetAsync()
    {
        var item = await _itemService.GetAsync(Id);
        Input = new CreateUpdateItemDto
        {
            Name = item.Name, Barcode = item.Barcode, Sku = item.Sku, ItemType = item.ItemType,
            CategoryId = item.CategoryId, CostPrice = item.CostPrice, SalePrice = item.SalePrice,
            AlertQuantity = item.AlertQuantity, IsActive = item.IsActive
        };
        try { Categories = await _masterService.GetAllCategoriesAsync(); } catch { }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            try { Categories = await _masterService.GetAllCategoriesAsync(); } catch { }
            return Page();
        }
        await _itemService.UpdateAsync(Id, Input);
        return RedirectToPage("/Items");
    }
}

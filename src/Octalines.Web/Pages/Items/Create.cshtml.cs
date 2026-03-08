using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages.Items;

public class CreateItemModel : PageModel
{
    private readonly IItemAppService _itemService;
    private readonly IMasterDataAppService _masterService;
    [BindProperty] public CreateUpdateItemDto Input { get; set; } = new() { IsActive = true };
    public List<CategoryDto> Categories { get; set; } = new();

    public CreateItemModel(IItemAppService itemService, IMasterDataAppService masterService)
    {
        _itemService = itemService;
        _masterService = masterService;
    }

    public async Task OnGetAsync()
    {
        try { Categories = await _masterService.GetAllCategoriesAsync(); } catch { }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            try { Categories = await _masterService.GetAllCategoriesAsync(); } catch { }
            return Page();
        }
        await _itemService.CreateAsync(Input);
        return RedirectToPage("/Items");
    }
}

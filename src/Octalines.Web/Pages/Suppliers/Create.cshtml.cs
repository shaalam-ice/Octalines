using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages.Suppliers;

public class CreateSupplierModel : PageModel
{
    private readonly IContactAppService _service;
    [BindProperty] public CreateUpdateContactDto Input { get; set; } = new() { ContactType = ContactType.Supplier, IsActive = true };
    public CreateSupplierModel(IContactAppService service) => _service = service;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        Input.ContactType = ContactType.Supplier;
        await _service.CreateAsync(Input);
        return RedirectToPage("/Suppliers");
    }
}

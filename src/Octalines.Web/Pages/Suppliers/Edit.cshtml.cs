using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages.Suppliers;

public class EditSupplierModel : PageModel
{
    private readonly IContactAppService _service;
    [BindProperty(SupportsGet = true)] public Guid Id { get; set; }
    [BindProperty] public CreateUpdateContactDto Input { get; set; } = new();

    public EditSupplierModel(IContactAppService service) => _service = service;

    public async Task OnGetAsync()
    {
        var c = await _service.GetAsync(Id);
        Input = new CreateUpdateContactDto { Name = c.Name, Phone = c.Phone, Email = c.Email, Address = c.Address, IsActive = c.IsActive, ContactType = c.ContactType };
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        Input.ContactType = ContactType.Supplier;
        await _service.UpdateAsync(Id, Input);
        return RedirectToPage("/Suppliers");
    }
}

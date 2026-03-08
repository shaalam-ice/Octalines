using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Octalines.AppServices;
using Octalines.Dtos;

namespace Octalines.Web.Pages.Customers;

public class CreateModel : PageModel
{
    private readonly IContactAppService _service;
    [BindProperty] public CreateUpdateContactDto Input { get; set; } = new() { ContactType = ContactType.Customer, IsActive = true };

    public CreateModel(IContactAppService service) => _service = service;

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();
        Input.ContactType = ContactType.Customer;
        await _service.CreateAsync(Input);
        return RedirectToPage("/Customers");
    }
}

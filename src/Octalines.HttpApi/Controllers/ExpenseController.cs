using Microsoft.AspNetCore.Mvc;
using Octalines.AppServices;
using Octalines.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Octalines.Controllers;

[RemoteService]
[Route("api/app/expenses")]
public class ExpenseController : AbpController, IExpenseAppService
{
    private readonly IExpenseAppService _service;
    public ExpenseController(IExpenseAppService service) => _service = service;

    [HttpGet] public Task<PagedResultDto<ExpenseDto>> GetListAsync(GetExpensesInput input) => _service.GetListAsync(input);
    [HttpGet("{id}")] public Task<ExpenseDto> GetAsync(Guid id) => _service.GetAsync(id);
    [HttpPost] public Task<ExpenseDto> CreateAsync(CreateUpdateExpenseDto input) => _service.CreateAsync(input);
    [HttpPut("{id}")] public Task<ExpenseDto> UpdateAsync(Guid id, CreateUpdateExpenseDto input) => _service.UpdateAsync(id, input);
    [HttpDelete("{id}")] public Task DeleteAsync(Guid id) => _service.DeleteAsync(id);
    [HttpGet("categories")] public Task<PagedResultDto<ExpenseCategoryDto>> GetCategoriesAsync(PagedAndSortedResultRequestDto input) => _service.GetCategoriesAsync(input);
    [HttpPost("categories")] public Task<ExpenseCategoryDto> CreateCategoryAsync(CreateUpdateExpenseCategoryDto input) => _service.CreateCategoryAsync(input);
    [HttpDelete("categories/{id}")] public Task DeleteCategoryAsync(Guid id) => _service.DeleteCategoryAsync(id);
}

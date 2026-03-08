using Octalines.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Octalines.AppServices;

public interface IExpenseAppService : IApplicationService
{
    Task<PagedResultDto<ExpenseDto>> GetListAsync(GetExpensesInput input);
    Task<ExpenseDto> GetAsync(Guid id);
    Task<ExpenseDto> CreateAsync(CreateUpdateExpenseDto input);
    Task<ExpenseDto> UpdateAsync(Guid id, CreateUpdateExpenseDto input);
    Task DeleteAsync(Guid id);
    Task<PagedResultDto<ExpenseCategoryDto>> GetCategoriesAsync(PagedAndSortedResultRequestDto input);
    Task<ExpenseCategoryDto> CreateCategoryAsync(CreateUpdateExpenseCategoryDto input);
    Task DeleteCategoryAsync(Guid id);
}

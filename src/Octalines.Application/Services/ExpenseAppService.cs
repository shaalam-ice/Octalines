using Microsoft.AspNetCore.Authorization;
using Octalines.AppServices;
using Octalines.Dtos;
using Octalines.Entities;
using Octalines.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Octalines.Services;

[Authorize(OctalinesPermissions.Expenses.Default)]
public class ExpenseAppService : ApplicationService, IExpenseAppService
{
    private readonly IRepository<Expense, Guid> _expenseRepo;
    private readonly IRepository<ExpenseCategory, Guid> _categoryRepo;
    private readonly IRepository<Account, Guid> _accountRepo;

    public ExpenseAppService(
        IRepository<Expense, Guid> expenseRepo,
        IRepository<ExpenseCategory, Guid> categoryRepo,
        IRepository<Account, Guid> accountRepo)
    {
        _expenseRepo = expenseRepo;
        _categoryRepo = categoryRepo;
        _accountRepo = accountRepo;
    }

    public async Task<PagedResultDto<ExpenseDto>> GetListAsync(GetExpensesInput input)
    {
        var query = await _expenseRepo.GetQueryableAsync();
        if (input.CategoryId.HasValue)
            query = query.Where(e => e.ExpenseCategoryId == input.CategoryId);
        if (input.StartDate.HasValue)
            query = query.Where(e => e.ExpenseDate >= input.StartDate);
        if (input.EndDate.HasValue)
            query = query.Where(e => e.ExpenseDate <= input.EndDate);

        var total = query.Count();
        var items = query.OrderByDescending(e => e.ExpenseDate).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

        var categories = await _categoryRepo.GetListAsync();
        var accounts = await _accountRepo.GetListAsync();

        var dtos = items.Select(e =>
        {
            var dto = ObjectMapper.Map<Expense, ExpenseDto>(e);
            dto.CategoryName = categories.FirstOrDefault(c => c.Id == e.ExpenseCategoryId)?.Name;
            dto.AccountName = accounts.FirstOrDefault(a => a.Id == e.AccountId)?.Name;
            return dto;
        }).ToList();
        return new PagedResultDto<ExpenseDto>(total, dtos);
    }

    public async Task<ExpenseDto> GetAsync(Guid id)
        => ObjectMapper.Map<Expense, ExpenseDto>(await _expenseRepo.GetAsync(id));

    [Authorize(OctalinesPermissions.Expenses.Create)]
    public async Task<ExpenseDto> CreateAsync(CreateUpdateExpenseDto input)
    {
        var expense = new Expense(GuidGenerator.Create());
        ObjectMapper.Map(input, expense);
        await _expenseRepo.InsertAsync(expense, autoSave: true);
        return ObjectMapper.Map<Expense, ExpenseDto>(expense);
    }

    [Authorize(OctalinesPermissions.Expenses.Edit)]
    public async Task<ExpenseDto> UpdateAsync(Guid id, CreateUpdateExpenseDto input)
    {
        var expense = await _expenseRepo.GetAsync(id);
        ObjectMapper.Map(input, expense);
        await _expenseRepo.UpdateAsync(expense, autoSave: true);
        return ObjectMapper.Map<Expense, ExpenseDto>(expense);
    }

    [Authorize(OctalinesPermissions.Expenses.Delete)]
    public async Task DeleteAsync(Guid id)
        => await _expenseRepo.DeleteAsync(id, autoSave: true);

    public async Task<PagedResultDto<ExpenseCategoryDto>> GetCategoriesAsync(PagedAndSortedResultRequestDto input)
    {
        var query = await _categoryRepo.GetQueryableAsync();
        var total = query.Count();
        var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        return new PagedResultDto<ExpenseCategoryDto>(total, ObjectMapper.Map<List<ExpenseCategory>, List<ExpenseCategoryDto>>(items));
    }

    public async Task<ExpenseCategoryDto> CreateCategoryAsync(CreateUpdateExpenseCategoryDto input)
    {
        var cat = new ExpenseCategory(GuidGenerator.Create());
        ObjectMapper.Map(input, cat);
        await _categoryRepo.InsertAsync(cat, autoSave: true);
        return ObjectMapper.Map<ExpenseCategory, ExpenseCategoryDto>(cat);
    }

    public async Task DeleteCategoryAsync(Guid id)
        => await _categoryRepo.DeleteAsync(id, autoSave: true);
}

using AutoMapper;
using Octalines.Dtos;
using Octalines.Entities;

namespace Octalines;

public class OctalinesApplicationAutoMapperProfile : Profile
{
    public OctalinesApplicationAutoMapperProfile()
    {
        CreateMap<Contact, ContactDto>();
        CreateMap<CreateUpdateContactDto, Contact>();

        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<CreateUpdateWarehouseDto, Warehouse>();

        CreateMap<Category, CategoryDto>();
        CreateMap<CreateUpdateCategoryDto, Category>();

        CreateMap<Brand, BrandDto>();
        CreateMap<CreateUpdateBrandDto, Brand>();

        CreateMap<Unit, UnitDto>();
        CreateMap<CreateUpdateUnitDto, Unit>();

        CreateMap<Tax, TaxDto>();
        CreateMap<CreateUpdateTaxDto, Tax>();

        CreateMap<Item, ItemDto>()
            .ForMember(d => d.CategoryName, o => o.Ignore())
            .ForMember(d => d.BrandName, o => o.Ignore())
            .ForMember(d => d.UnitName, o => o.Ignore())
            .ForMember(d => d.TaxRate, o => o.Ignore());

        CreateMap<Sale, SaleDto>()
            .ForMember(d => d.CustomerName, o => o.Ignore())
            .ForMember(d => d.WarehouseName, o => o.Ignore());
        CreateMap<SaleLine, SaleLineDto>();
        CreateMap<SalePayment, SalePaymentDto>();

        CreateMap<SaleReturn, SaleReturnDto>();
        CreateMap<SaleReturnLine, SaleReturnLineDto>();

        CreateMap<Purchase, PurchaseDto>()
            .ForMember(d => d.SupplierName, o => o.Ignore())
            .ForMember(d => d.WarehouseName, o => o.Ignore());
        CreateMap<PurchaseLine, PurchaseLineDto>();
        CreateMap<PurchasePayment, PurchasePaymentDto>();

        CreateMap<Account, AccountDto>();
        CreateMap<CreateUpdateAccountDto, Account>();

        CreateMap<MoneyTransfer, MoneyTransferDto>()
            .ForMember(d => d.FromAccountName, o => o.Ignore())
            .ForMember(d => d.ToAccountName, o => o.Ignore());

        CreateMap<Deposit, DepositDto>()
            .ForMember(d => d.AccountName, o => o.Ignore());

        CreateMap<ExpenseCategory, ExpenseCategoryDto>();
        CreateMap<CreateUpdateExpenseCategoryDto, ExpenseCategory>();
        CreateMap<Expense, ExpenseDto>()
            .ForMember(d => d.CategoryName, o => o.Ignore())
            .ForMember(d => d.AccountName, o => o.Ignore());
        CreateMap<CreateUpdateExpenseDto, Expense>();

        CreateMap<Coupon, CouponDto>();
        CreateMap<CreateUpdateCouponDto, Coupon>();
        CreateMap<CustomerCoupon, CustomerCouponDto>()
            .ForMember(d => d.CustomerName, o => o.Ignore());

        CreateMap<Quotation, QuotationDto>()
            .ForMember(d => d.CustomerName, o => o.Ignore());
        CreateMap<QuotationLine, QuotationLineDto>();

        CreateMap<AdvancePayment, AdvancePaymentDto>()
            .ForMember(d => d.ContactName, o => o.Ignore());

        CreateMap<StockAdjustment, StockAdjustmentDto>()
            .ForMember(d => d.ItemName, o => o.Ignore())
            .ForMember(d => d.WarehouseName, o => o.Ignore());

        CreateMap<StockTransfer, StockTransferDto>()
            .ForMember(d => d.ItemName, o => o.Ignore())
            .ForMember(d => d.FromWarehouseName, o => o.Ignore())
            .ForMember(d => d.ToWarehouseName, o => o.Ignore());

        CreateMap<StockLedger, StockLedgerDto>()
            .ForMember(d => d.ItemName, o => o.Ignore())
            .ForMember(d => d.WarehouseName, o => o.Ignore());

        CreateMap<MessageTemplate, MessageTemplateDto>();
        CreateMap<CreateUpdateMessageTemplateDto, MessageTemplate>();
    }
}

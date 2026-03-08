using Microsoft.EntityFrameworkCore;
using Octalines.Entities;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace Octalines.EntityFrameworkCore;

public class OctalinesDbContext : AbpDbContext<OctalinesDbContext>,
    IIdentityDbContext,
    IPermissionManagementDbContext,
    ISettingManagementDbContext,
    IAuditLoggingDbContext
{
    // ABP built-in tables
    public DbSet<Volo.Abp.Identity.IdentityUser> Users { get; set; }
    public DbSet<Volo.Abp.Identity.IdentityRole> Roles { get; set; }
    public DbSet<Volo.Abp.Identity.IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<Volo.Abp.Identity.OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<Volo.Abp.Identity.IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<Volo.Abp.Identity.IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<Volo.Abp.Identity.IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<Volo.Abp.Identity.IdentitySession> Sessions { get; set; }
    public DbSet<Volo.Abp.PermissionManagement.PermissionGroupDefinitionRecord> PermissionGroups { get; set; }
    public DbSet<Volo.Abp.PermissionManagement.PermissionDefinitionRecord> Permissions { get; set; }
    public DbSet<Volo.Abp.PermissionManagement.PermissionGrant> PermissionGrants { get; set; }
    public DbSet<Volo.Abp.PermissionManagement.ResourcePermissionGrant> ResourcePermissionGrants { get; set; }
    public DbSet<Volo.Abp.SettingManagement.Setting> Settings { get; set; }
    public DbSet<Volo.Abp.SettingManagement.SettingDefinitionRecord> SettingDefinitionRecords { get; set; }
    public DbSet<Volo.Abp.AuditLogging.AuditLog> AuditLogs { get; set; }
    public DbSet<Volo.Abp.AuditLogging.AuditLogExcelFile> AuditLogExcelFiles { get; set; }

    // Business tables
    public DbSet<StoreSettings> StoreSettings { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Tax> Taxes { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<StockLedger> StockLedgers { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleLine> SaleLines { get; set; }
    public DbSet<SalePayment> SalePayments { get; set; }
    public DbSet<SaleReturn> SaleReturns { get; set; }
    public DbSet<SaleReturnLine> SaleReturnLines { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseLine> PurchaseLines { get; set; }
    public DbSet<PurchasePayment> PurchasePayments { get; set; }
    public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
    public DbSet<PurchaseReturnLine> PurchaseReturnLines { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountTransaction> AccountTransactions { get; set; }
    public DbSet<MoneyTransfer> MoneyTransfers { get; set; }
    public DbSet<Deposit> Deposits { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<CustomerCoupon> CustomerCoupons { get; set; }
    public DbSet<Quotation> Quotations { get; set; }
    public DbSet<QuotationLine> QuotationLines { get; set; }
    public DbSet<AdvancePayment> AdvancePayments { get; set; }
    public DbSet<StockAdjustment> StockAdjustments { get; set; }
    public DbSet<StockTransfer> StockTransfers { get; set; }
    public DbSet<MessageTemplate> MessageTemplates { get; set; }
    public DbSet<MessageLog> MessageLogs { get; set; }

    public OctalinesDbContext(DbContextOptions<OctalinesDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureIdentity();
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureAuditLogging();

        // Business entities config
        builder.Entity<StoreSettings>(b => { b.ToTable("OctlStoreSettings"); });

        builder.Entity<Contact>(b =>
        {
            b.ToTable("OctlContacts");
            b.Property(c => c.Name).IsRequired().HasMaxLength(200);
            b.Property(c => c.Email).HasMaxLength(200);
            b.Property(c => c.Phone).HasMaxLength(50);
            b.HasIndex(c => c.ContactType);
        });

        builder.Entity<Warehouse>(b =>
        {
            b.ToTable("OctlWarehouses");
            b.Property(w => w.Name).IsRequired().HasMaxLength(200);
        });

        builder.Entity<Category>(b =>
        {
            b.ToTable("OctlCategories");
            b.Property(c => c.Name).IsRequired().HasMaxLength(200);
        });

        builder.Entity<Brand>(b =>
        {
            b.ToTable("OctlBrands");
            b.Property(b2 => b2.Name).IsRequired().HasMaxLength(200);
        });

        builder.Entity<Unit>(b =>
        {
            b.ToTable("OctlUnits");
            b.Property(u => u.Name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<Tax>(b =>
        {
            b.ToTable("OctlTaxes");
            b.Property(t => t.Name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<PaymentType>(b =>
        {
            b.ToTable("OctlPaymentTypes");
            b.Property(p => p.Name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<Item>(b =>
        {
            b.ToTable("OctlItems");
            b.Property(i => i.Name).IsRequired().HasMaxLength(300);
            b.Property(i => i.Barcode).HasMaxLength(100);
            b.HasIndex(i => i.Barcode);
        });

        builder.Entity<StockLedger>(b =>
        {
            b.ToTable("OctlStockLedgers");
            b.HasIndex(sl => sl.ItemId);
            b.HasIndex(sl => sl.WarehouseId);
        });

        builder.Entity<Sale>(b =>
        {
            b.ToTable("OctlSales");
            b.Property(s => s.InvoiceNumber).IsRequired().HasMaxLength(50);
            b.HasIndex(s => s.InvoiceNumber).IsUnique();
            b.HasMany(s => s.Lines).WithOne().HasForeignKey(l => l.SaleId).OnDelete(DeleteBehavior.Cascade);
            b.HasMany(s => s.Payments).WithOne().HasForeignKey(p => p.SaleId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<SaleLine>(b => { b.ToTable("OctlSaleLines"); });
        builder.Entity<SalePayment>(b => { b.ToTable("OctlSalePayments"); });

        builder.Entity<SaleReturn>(b =>
        {
            b.ToTable("OctlSaleReturns");
            b.HasMany(r => r.Lines).WithOne().HasForeignKey(l => l.SaleReturnId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<SaleReturnLine>(b => { b.ToTable("OctlSaleReturnLines"); });

        builder.Entity<Purchase>(b =>
        {
            b.ToTable("OctlPurchases");
            b.Property(p => p.PurchaseNumber).IsRequired().HasMaxLength(50);
            b.HasMany(p => p.Lines).WithOne().HasForeignKey(l => l.PurchaseId).OnDelete(DeleteBehavior.Cascade);
            b.HasMany(p => p.Payments).WithOne().HasForeignKey(py => py.PurchaseId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<PurchaseLine>(b => { b.ToTable("OctlPurchaseLines"); });
        builder.Entity<PurchasePayment>(b => { b.ToTable("OctlPurchasePayments"); });

        builder.Entity<PurchaseReturn>(b =>
        {
            b.ToTable("OctlPurchaseReturns");
            b.HasMany(r => r.Lines).WithOne().HasForeignKey(l => l.PurchaseReturnId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<PurchaseReturnLine>(b => { b.ToTable("OctlPurchaseReturnLines"); });

        builder.Entity<Account>(b =>
        {
            b.ToTable("OctlAccounts");
            b.Property(a => a.Name).IsRequired().HasMaxLength(200);
        });

        builder.Entity<AccountTransaction>(b =>
        {
            b.ToTable("OctlAccountTransactions");
            b.HasIndex(t => t.AccountId);
        });

        builder.Entity<MoneyTransfer>(b => { b.ToTable("OctlMoneyTransfers"); });
        builder.Entity<Deposit>(b => { b.ToTable("OctlDeposits"); });

        builder.Entity<Expense>(b =>
        {
            b.ToTable("OctlExpenses");
            b.HasIndex(e => e.ExpenseCategoryId);
        });

        builder.Entity<ExpenseCategory>(b =>
        {
            b.ToTable("OctlExpenseCategories");
            b.Property(ec => ec.Name).IsRequired().HasMaxLength(200);
        });

        builder.Entity<Coupon>(b =>
        {
            b.ToTable("OctlCoupons");
            b.Property(c => c.Code).IsRequired().HasMaxLength(50);
            b.HasIndex(c => c.Code).IsUnique();
        });

        builder.Entity<CustomerCoupon>(b =>
        {
            b.ToTable("OctlCustomerCoupons");
            b.Property(cc => cc.Code).IsRequired().HasMaxLength(50);
        });

        builder.Entity<Quotation>(b =>
        {
            b.ToTable("OctlQuotations");
            b.HasMany(q => q.Lines).WithOne().HasForeignKey(l => l.QuotationId).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<QuotationLine>(b => { b.ToTable("OctlQuotationLines"); });

        builder.Entity<AdvancePayment>(b => { b.ToTable("OctlAdvancePayments"); });

        builder.Entity<StockAdjustment>(b =>
        {
            b.ToTable("OctlStockAdjustments");
            b.HasIndex(a => a.ItemId);
        });

        builder.Entity<StockTransfer>(b =>
        {
            b.ToTable("OctlStockTransfers");
            b.HasIndex(t => t.ItemId);
        });

        builder.Entity<MessageTemplate>(b =>
        {
            b.ToTable("OctlMessageTemplates");
            b.Property(m => m.Name).IsRequired().HasMaxLength(200);
        });

        builder.Entity<MessageLog>(b =>
        {
            b.ToTable("OctlMessageLogs");
            b.Property(m => m.RecipientPhone).IsRequired().HasMaxLength(50);
        });
    }
}

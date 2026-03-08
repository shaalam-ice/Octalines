using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Octalines.Web.Pages;

public class ReportsModel : PageModel
{
    public List<ReportLink> ReportLinks { get; } = new()
    {
        new("Profit & Loss", "fas fa-chart-line", "P&L summary"),
        new("Sales Report", "fas fa-receipt", "All sales with totals"),
        new("Purchase Report", "fas fa-shopping-cart", "All purchases"),
        new("Sales Return", "fas fa-undo", "Returned sales"),
        new("Purchase Return", "fas fa-undo-alt", "Purchase returns"),
        new("Expense Report", "fas fa-money-bill", "All expenses"),
        new("Stock Report", "fas fa-boxes", "Current stock levels"),
        new("Customer Orders", "fas fa-users", "Orders per customer"),
        new("Sales Tax", "fas fa-percentage", "Tax collected on sales"),
        new("Purchase Tax", "fas fa-percentage", "Tax on purchases"),
        new("Stock Ledger", "fas fa-list", "Stock movements log"),
        new("Sales Payments", "fas fa-credit-card", "Sales payment history"),
        new("Purchase Payments", "fas fa-credit-card", "Purchase payment history"),
        new("Seller Points", "fas fa-star", "Seller performance"),
        new("Return Items", "fas fa-exchange-alt", "Returned items detail"),
        new("Supplier Items", "fas fa-truck", "Items per supplier"),
    };
}

public record ReportLink(string Title, string Icon, string Description);

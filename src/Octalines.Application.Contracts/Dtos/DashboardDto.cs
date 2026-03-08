namespace Octalines.Dtos;

public class DashboardSummaryDto
{
    public decimal TodaySales { get; set; }
    public decimal ThisMonthSales { get; set; }
    public int TotalCustomers { get; set; }
    public int TotalSuppliers { get; set; }
    public int LowStockItems { get; set; }
    public decimal CashBalance { get; set; }
    public List<DailySalesDto> DailySales { get; set; } = new();
    public List<TopItemDto> TopItems { get; set; } = new();
}

public class DailySalesDto
{
    public DateTime Date { get; set; }
    public decimal Total { get; set; }
}

public class TopItemDto
{
    public string ItemName { get; set; } = string.Empty;
    public decimal TotalQuantity { get; set; }
    public decimal TotalRevenue { get; set; }
}

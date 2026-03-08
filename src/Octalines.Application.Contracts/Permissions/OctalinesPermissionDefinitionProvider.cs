using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Octalines.Permissions;

public class OctalinesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(OctalinesPermissions.GroupName, L("Permission:Octalines"));

        group.AddPermission(OctalinesPermissions.Dashboard.Default, L("Permission:Dashboard"));

        AddCrudPermissions(group, OctalinesPermissions.Customers.Default, "Customers");
        AddCrudPermissions(group, OctalinesPermissions.Suppliers.Default, "Suppliers");
        AddCrudPermissions(group, OctalinesPermissions.Items.Default, "Items");
        AddCrudPermissions(group, OctalinesPermissions.Sales.Default, "Sales");
        AddCrudPermissions(group, OctalinesPermissions.Purchases.Default, "Purchases");
        AddCrudPermissions(group, OctalinesPermissions.Accounts.Default, "Accounts");
        AddCrudPermissions(group, OctalinesPermissions.Expenses.Default, "Expenses");
        AddCrudPermissions(group, OctalinesPermissions.Coupons.Default, "Coupons");
        AddCrudPermissions(group, OctalinesPermissions.Quotations.Default, "Quotations");
        AddCrudPermissions(group, OctalinesPermissions.Warehouses.Default, "Warehouses");

        group.AddPermission(OctalinesPermissions.Reports.Default, L("Permission:Reports"));
        group.AddPermission(OctalinesPermissions.Settings.Default, L("Permission:Settings"));

        var stock = group.AddPermission(OctalinesPermissions.Stock.Default, L("Permission:Stock"));
        stock.AddChild(OctalinesPermissions.Stock.Create, L("Permission:Stock.Create"));

        var messaging = group.AddPermission(OctalinesPermissions.Messaging.Default, L("Permission:Messaging"));
        messaging.AddChild(OctalinesPermissions.Messaging.Send, L("Permission:Messaging.Send"));
    }

    private static void AddCrudPermissions(PermissionGroupDefinition group, string defaultPermission, string displayName)
    {
        var parent = group.AddPermission(defaultPermission, L($"Permission:{displayName}"));
        parent.AddChild(defaultPermission + ".Create", L($"Permission:{displayName}.Create"));
        parent.AddChild(defaultPermission + ".Edit", L($"Permission:{displayName}.Edit"));
        parent.AddChild(defaultPermission + ".Delete", L($"Permission:{displayName}.Delete"));
    }

    private static LocalizableString L(string name) => LocalizableString.Create<OctalinesResource>(name);
}

namespace Octalines.Permissions;

public static class OctalinesPermissions
{
    public const string GroupName = "Octalines";

    public static class Dashboard { public const string Default = GroupName + ".Dashboard"; }

    public static class Customers
    {
        public const string Default = GroupName + ".Customers";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Suppliers
    {
        public const string Default = GroupName + ".Suppliers";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Items
    {
        public const string Default = GroupName + ".Items";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Sales
    {
        public const string Default = GroupName + ".Sales";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Purchases
    {
        public const string Default = GroupName + ".Purchases";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Accounts
    {
        public const string Default = GroupName + ".Accounts";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Expenses
    {
        public const string Default = GroupName + ".Expenses";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Coupons
    {
        public const string Default = GroupName + ".Coupons";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Quotations
    {
        public const string Default = GroupName + ".Quotations";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Reports { public const string Default = GroupName + ".Reports"; }
    public static class Settings { public const string Default = GroupName + ".Settings"; }
    public static class Warehouses
    {
        public const string Default = GroupName + ".Warehouses";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Stock
    {
        public const string Default = GroupName + ".Stock";
        public const string Create = Default + ".Create";
    }
    public static class Messaging
    {
        public const string Default = GroupName + ".Messaging";
        public const string Send = Default + ".Send";
    }
}

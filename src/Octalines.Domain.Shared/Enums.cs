namespace Octalines;
public enum ContactType { Customer, Supplier }
public enum PaymentMethod { Cash, Card, MobileWallet, Advance, Coupon }
public enum SaleStatus { Draft, Completed, Returned, PartiallyReturned }
public enum PurchaseStatus { Draft, Completed, Returned, PartiallyReturned }
public enum StockMovementType { SaleDeduction, PurchaseAddition, SaleReturn, PurchaseReturn, ManualAdjustment, WarehouseTransferOut, WarehouseTransferIn }
public enum AccountType { Cash, Bank, MobileWallet }
public enum TransactionType { Income, Expense, Transfer, Deposit }
public enum ItemType { Product, Service }
public enum AdjustmentType { Increase, Decrease }
public enum AdvanceType { CustomerAdvance, SupplierAdvance }
public enum MessageChannel { SMS, WhatsApp }

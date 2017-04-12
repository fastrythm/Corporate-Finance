using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Helper.Structure
{

    public struct MessageClass
    {
        public static string Success = "alert-success success";
        public static string Error = "alert-error error";
        public static string ErrorAndHide = "alert-error hide";
    }

    public class ExcelFileProcess
    {
        public string Message { get; set; }
        public bool Response { get; set; }
        public string MessageType { get; set; }
    }

    public struct UserCardExpenseEnum
    {
        public static string Date = "Date";
        public static string Receipt_Number = "Receipt Number";
        public static string Account_Number = "Account Number";
        public static string Description = "Description";
        public static string Bill_Type = "Bill Type";
        public static string Card_Member = "Card Member";
        public static string Payment_Detail = "Payment Detail";
        public static string Expense_Head = "Expense Head";
        public static string BE_NBE = "BE/NBE";
        public static string Amount = "Amount";
        public static string Sales_Related = "Sales Related";
        public static string Transaction_Type = "Transaction Type";
        public static string Client = "Client";
        public static string Client_Number = "Client Number";
        public static string Consultant = "Consultant";
        public static string Consultant_Number = "Consultant Number";
        public static string Remarks = "Remarks";
    }

    public struct CompanyType
    {
        public static string Inter = "Inter";
        public static string Owner = "Owner";
        public static string Vendor = "Vendor";
        public static string Client = "Client";
    }

    public enum UserAppPermissions
    {
        PettyCash_View = 1,
        PettyCash_Add = 2,
        PettyCash_Edit = 3,
        PettyCash_Delete = 4,

        BankPosition_View = 5,
        BankPosition_Add = 6,
        BankPosition_Edit = 7,
        BankPosition_Delete = 8,

        UserCardExpense_View = 9,
        UserCardExpense_Add = 10,
        UserCardExpense_Edit = 11,
        UserCardExpense_Delete = 12,

        Compliance_View = 13,
        Compliance_Add = 14,
        Compliance_Edit = 15,
        Compliance_Delete = 16,

        User_View = 17,
        User_Add = 18,
        User_Edit = 19,
        User_Delete = 20,

        BankTransaction_View = 21,
        BankTransaction_Add = 22,
        BankTransaction_Edit = 23,
        BankTransaction_Delete = 24,
    }

    public class ChartOfAccount
    {
        public static List<string> Titles = new List<string>(){
         "Cash and Bank Balances:Petty Imprest",
        "Advertising",
        "Communication Expense",
        "Cost of Marketing",
        "Cost of Marketing:Advertising",
        "Dues & Subscription",
        "Dues & Subscription:Accounting Software Fee",
        "Dues & Subscription:Membership/Professional Dues",
        "Dues & Subscription:Newspaper/Magazine Subscription",
        "Dues and subscriptions",
        "Electricity",
        "Entertainments",
        "Equipment rental",
        "Gas Charges",
        "General Insurance",
        "Generator Fuel",
        "Groceries/Kitchenware",
        "Incentives",
        "Internet / DSL",
        "IT Accessories",
        "Janitorial Services",
        "Legal and professional fees",
        "Lodging",
        "Loss on discontinued operations, net of tax",
        "Management compensation",
        "Management Fee",
        "Management Fee:Intercompany Management Fee",
        "Meals & Entertainment",
        "Meals and entertainment",
        "Miscelleaneous",
        "Mobile Allowance",
        "Mobile communication",
        "Office expenses",
        "Office Supplies",
        "Other Admin & OPEX",
        "Other Expenses",
        "Other Expenses:Amortization of Goodwill",
        "Other Expenses:Donation / Charity",
        "Other Expenses:Miscellaneous",
        "Other Expenses:Office Expenses",
        "Other Expenses:PO Box Rental",
        "Other Expenses:Security Services Expense",
        "Other General and Admin Expenses",
        "Other general and administrative expenses",
        "Other Interest Expense",
        "Other selling expenses",
        "Other Types of Expenses-Advertising Expenses",
        "Parking/Toll",
        "Payroll - Contract staff Wages",
        "Payroll - Fuel reimbursement",
        "Payroll - Leave Encashment",
        "Payroll - Mobile Allowance",
        "Postage / Shipping expense",
        "Promotional",
        "Purchases",
        "Rent or Lease",
        "Repairs and Maintenance",
        "Repairs and Maintenance:Repair & Maintenance",
        "Repairs and Maintenance:Repair & Maintenance:Fuel Reimbursement (Others)",
        "Salaries - Permanent",
        "Security Expense",
        "Shipping and delivery expense",
        "Software/System Maintenance",
        "Staff Welfare & Gifts",
        "Stationery & Printing",
        "Stationery and printing",
        "Supplies / Stationaries",
        "Telephone",
        "Training & Development",
        "Training & Development:Conference/Trainings",
        "Transportation",
        "Transportation & Conveyance",
        "Travel expenses - general and admin expenses",
        "Travel expenses - selling expense",
        "Travel expenses - selling expenses",
        "Travel Meals",
        "Travelling & Lodging",
        "Travelling - Management",
        "Utilities:Building Maintenance Charges",
        "Utilities:Generator Rent",
        "Utilities:Parking",
        "Utilities:Telephone - Cell",
        "Wage expenses",
        "Water Tanker / Maintenance"

    };

    }


}

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
    }

   

}

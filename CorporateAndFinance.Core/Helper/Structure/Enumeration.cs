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

}

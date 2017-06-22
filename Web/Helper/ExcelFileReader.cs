using CorporateAndFinance.Core.Helper.Structure;
using Excel;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using CorporateAndFinance.Core.Model;

namespace CorporateAndFinance.Web.Helper
{
    public class ExcelFileReader
    {
        static Regex regexDateFormat = new Regex(@"^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$");
        static Regex regexNumberFormat = new Regex(@"^[0-9]*$");
        static Regex regexCurrencyFormat = new Regex(@"^-?[0-9]\d*(\.\d+)?$");

        public static DataSet Read(string filePath, bool IsFirstRowAsColumnNames = true)
        {
            System.IO.File.GetAccessControl(filePath);
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader reader = null;
            if (Path.GetExtension(filePath).ToLower().Equals(".xls"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(stream);
            }
            else if (Path.GetExtension(filePath).ToLower().Equals(".xlsx"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            reader.IsFirstRowAsColumnNames = IsFirstRowAsColumnNames;

            DataSet result = reader.AsDataSet();
            reader.Close();
            return result;
        }

        public static string CheckAllColumnExist(DataTable table, List<string> columnNames)
        {
            DataColumnCollection columns = table.Columns;

            foreach (string column in columnNames)
            {
                if (!columns.Contains(column))
                {
                    return column;
                }
            }

            return string.Empty;

        }

        public static string ValidateData(DataTable dataTable, List<string> mandatoryColumnList)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                foreach (string column in mandatoryColumnList)
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(dataTable.Rows[i][column])))
                    {
                        sb.AppendFormat(Messages.MSG_GENERIC_UPLOAD_VALIDATION_ERROR, column, i);
                    }
                }

            }

            return sb.ToString();
        }


        public static string ValidateDataFormat(DataTable dataTable)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                try
                {
                    DateTime.Parse(dataTable.Rows[i][UserCardExpenseEnum.Date].ToString());
                }
                catch (Exception ex)
                {
                    sb.AppendFormat(Messages.MSG_UPLOAD_INVALID_DATE_FORMAT, UserCardExpenseEnum.Date, i + 2);
                }


                if (!string.IsNullOrWhiteSpace(Convert.ToString(dataTable.Rows[i][UserCardExpenseEnum.Client_Number])) && !regexNumberFormat.IsMatch(dataTable.Rows[i][UserCardExpenseEnum.Client_Number].ToString()))
                {
                    sb.AppendFormat(Messages.MSG_UPLOAD_INVALID_NUMBER_FORMAT, UserCardExpenseEnum.Client_Number, i + 2);
                }

                if (!string.IsNullOrWhiteSpace(Convert.ToString(dataTable.Rows[i][UserCardExpenseEnum.Consultant_Number])) && !regexNumberFormat.IsMatch(dataTable.Rows[i][UserCardExpenseEnum.Consultant_Number].ToString()))
                {
                    sb.AppendFormat(Messages.MSG_UPLOAD_INVALID_NUMBER_FORMAT, UserCardExpenseEnum.Consultant_Number, i + 2);
                }

                if (!regexCurrencyFormat.IsMatch(dataTable.Rows[i][UserCardExpenseEnum.Amount].ToString()))
                {
                    sb.AppendFormat(Messages.MSG_UPLOAD_INVALID_DECIMAL_FORMAT, UserCardExpenseEnum.Amount, i + 2);
                }

            }

            return sb.ToString();
        }

        public static string ValidateAccountNumber(DataTable dataTable, IEnumerable<UserCard> userCards)
        {
            StringBuilder sb = new StringBuilder();
            
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                var isFound = userCards.Select(x => x.CardNumber.ToLower() == dataTable.Rows[i][UserCardExpenseEnum.Account_Number].ToString().ToLower()).FirstOrDefault();
                if (!isFound)
                {
                    sb.AppendFormat(Messages.MSG_UPLOAD_ACCOUNT_NUMBER_NOTASSIGN, dataTable.Rows[i][UserCardExpenseEnum.Account_Number].ToString(), i + 2);
                }
            }

            return sb.ToString();
        }

        public static string ValidateAmount(DataTable dataTable, List<string> ValidateColumnList)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                foreach (string column in ValidateColumnList)
                {
                    if (!string.IsNullOrWhiteSpace(Convert.ToString(dataTable.Rows[i][column])) && !Convert.ToString(dataTable.Rows[i][column]).Equals("-") &&  !regexCurrencyFormat.IsMatch(dataTable.Rows[i][column].ToString()))
                    {
                        sb.AppendFormat(Messages.MSG_UPLOAD_INVALID_DECIMAL_FORMAT, column, i + 2);
                    }
                }
            }

            return sb.ToString();
        }

        public static string ValidateDepartmentAndUsers(DataTable dataTable,IEnumerable<Department> deparments, List<ApplicationUser> users)
        {

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string department = Convert.ToString(dataTable.Rows[i][UserExpenseMandatoryColumn.Department]);
                string employeeNumber = Convert.ToString(dataTable.Rows[i][UserExpenseMandatoryColumn.Employee_Number]);
                string employeeName = Convert.ToString(dataTable.Rows[i][UserExpenseMandatoryColumn.Employee_Name]);
                var dept = deparments.Where(x => x.Name == department).SingleOrDefault();
                 if (dept == null)
                 {
                        sb.AppendFormat(Messages.MSG_UPLOAD_DEPARTMENT_NOT_FOUND, department, i + 2);
                 }
                var user = users.Where(x => x.EmployeeNumber == employeeNumber).SingleOrDefault();
                if (user == null)
                {
                    sb.AppendFormat(Messages.MSG_UPLOAD_USER_NOT_FOUND, employeeNumber, employeeName,  i + 2);
                }
            }

            return sb.ToString();
        }
    }
}
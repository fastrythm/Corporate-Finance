using Excel;
using Resources;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CorporateAndFinance.Web.Helper
{
    public class ExcelFileReader
    {
        public static DataSet Read(string filePath, bool IsFirstRowAsColumnNames = true)
        {
            System.IO.File.GetAccessControl(filePath);
            FileStream stream =  File.Open(filePath, FileMode.Open, FileAccess.Read);
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
            for(int i=0;i<dataTable.Rows.Count;i++)
            {
                foreach(string column in mandatoryColumnList)
                {
                    if (string.IsNullOrWhiteSpace(Convert.ToString(dataTable.Rows[i][column])))
                    {
                        sb.AppendFormat(Messages.MSG_GENERIC_UPLOAD_VALIDATION_ERROR, column,i);
                    }
                }
                
            }

            return sb.ToString();
        }
    }
}
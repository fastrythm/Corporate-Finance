using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Helper.Structure
{
    public class Utility
    {
        public static string CovertID(string value,string trimValue)
        {
            if(string.IsNullOrWhiteSpace(value))
                return string.Empty;

            string id = value.Replace(trimValue, "");
            long number;
            if(long.TryParse(id,out number))
            {
                return number.ToString();
            }

            return string.Empty;
        }

        public static string FormatedId (string label,string Id)
        {
            return label + Convert.ToInt64(Id).ToString("000000");
        }
    }
}

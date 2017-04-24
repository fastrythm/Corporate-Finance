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
    }
}

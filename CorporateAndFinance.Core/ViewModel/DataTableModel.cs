using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class DataTablesViewModel
    {
        public IEnumerable<DTColumns> columns { get; set; }
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public IEnumerable<DTOrder> order { get; set; }
        public DTSearch search { get; set; }
        public IEnumerable<DTaoData> aoData { get; set; }

        public int TotalRecordsCount { get; set; }
    }
    public class DTSearch
    {
        public bool regex { get; set; }
        public string value { get; set; }
    }

    public class DTColumns
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public DTSearch search { get; set; }
    }

    public class DTOrder
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DTaoData
    {
        public string name { get; set; }
        public string value { get; set; }
    }

}

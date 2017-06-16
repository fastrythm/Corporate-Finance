using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Model
{
    public class Expense
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ExpenseID { get; set; }
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Type { get; set; }

        [DefaultValue("True")]
        public bool IsActive { get; set; }

        public int SortOrder { get; set; }
       
    }
}

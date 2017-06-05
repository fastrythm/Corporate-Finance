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
    [Table("Department")]
    public partial class Department
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DepartmentID { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(20)]
        public string ShortName { get; set; }
        [StringLength(50)]
        public string DepartmentType { get; set; }

        [DefaultValue("True")]
        public bool IsActive { get; set; }

        public Guid CompanyID { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

        public virtual Company Company { get; set; }

      //  public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<UserExpense> UserExpenses { get; set; }
    }
}

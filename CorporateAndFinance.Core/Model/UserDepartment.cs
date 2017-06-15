using CorporateAndFinance.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace CorporateAndFinance.Core.Model
{
    [Table("UserDepartment")]
    public class UserDepartment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserDepartmentID { get; set; }
        public string UserID { get; set; }
        [Display(Name = "Department")]
        public long DepartmentID { get; set; }
        public string RoleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [DefaultValue("True")]
        public bool IsActive { get; set; }
        [DefaultValue("False")]
        public bool IsPrimary { get; set; }
        public Guid CreatedBy { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser Users { get; set; }
        public virtual Department Department { get; set; }

        [NotMapped]
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        [NotMapped]
        [Display(Name = "Role")]
        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}

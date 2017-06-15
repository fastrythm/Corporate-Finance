using CorporateAndFinance.Core.ViewModel;
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
    [Table("Requisition")]
    public class Requisition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RequisitionID { get; set; }
        public bool IsBudgeted { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RequisitionDate { get; set; } = DateTime.Now;
        public string RequestNumber { get; set; }
        public long DepartmentID { get; set; }
        public string ClientServices { get; set; }
        public string Division { get; set; }
        public string NewProject { get; set; }

        public string Tenure { get; set; }
        public DateTime? StartDate { get; set; }
        public string JobTitle { get; set; }
        public string GradeLevel { get; set; }
        public string ReportedToJobTitle { get; set; }
        public bool IsTravelling { get; set; }
        public int NoOfPosition { get; set; }
        public string AdditionalBenefits { get; set; }
        public string TypeOfEmployement { get; set; }
        public string Duration { get; set; }

        public bool IsNewPosition { get; set; }

        public string Reason { get; set; }

        public string JobProfileFilePath { get; set; }

        public string OtherDetail { get; set; }
        public bool IsBillable { get; set; }

        public decimal ProjectedRevenue { get; set; }
        public decimal AllocatedBudget { get; set; }

        public bool HasLaptopPC { get; set; }
        public string LaptopDetails { get; set; }
        public bool HasHeadset { get; set; }
        public bool HasMouse { get; set; }
        public bool HasEmail { get; set; }
        public string EmailDetails { get; set; }
        public bool HasUSDID { get; set; }
        public bool HasInternetAccess { get; set; }
        public string InternetAccessDetails { get; set; }
        public string ApplicationSoftwareSecurityDetails { get; set; }
        [DefaultValue("False")]
        public bool IsDeleted { get; set; }
        public string Status { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        [NotMapped]
        public IEnumerable<UserDepartmentVM> Departments { get; set; }
    }
}

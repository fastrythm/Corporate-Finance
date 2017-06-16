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
        [DisplayName("Is Budgeted")]
        public bool IsBudgeted { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        public DateTime RequisitionDate { get; set; } = DateTime.Now;
        [DisplayName("Ref: No.")]
        public string RequestNumber { get; set; }
        [DisplayName("Department")]
        public long DepartmentID { get; set; }
        [DisplayName("Client Services")]
        public string ClientServices { get; set; }
       
        public string Division { get; set; }
        [DisplayName("New Project")]
        public string NewProject { get; set; }

        public string Tenure { get; set; }
        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }
        [Required]
        [DisplayName("Job Title")]
        public string JobTitle { get; set; }
        [Required]
        [DisplayName("Grade Level")]
        public string GradeLevel { get; set; }
        [Required]
        [DisplayName("Reported To Job Title")]
        public string ReportedToJobTitle { get; set; }
        [DisplayName("Is Travelling")]
        public bool IsTravelling { get; set; }
        [Required]
        [Range(1, 50)]
        [DisplayName("No Of Position")]
        public int NoOfPosition { get; set; }
        [DisplayName("Additional Benefits")]
        public string AdditionalBenefits { get; set; }
        [DisplayName("Type Of Employement")]
        public string TypeOfEmployement { get; set; }
        public string Duration { get; set; }
        [DisplayName("Is New Position")]
        public bool IsNewPosition { get; set; }
     
        public string Reason { get; set; }
        [DisplayName("Attach a copy of revised organogram")]
        public string RevisedOrganogram { get; set; }

        [DisplayName("Name of person being replaced")]
        public string ReplacePersonName { get; set; }
        [DisplayName("Replace person job title")]
        public string ReplacePersonJobTitle { get; set; }

        [DisplayName("Job Profile")]
        public string JobProfileFilePath { get; set; }
        [DisplayName("Other Detail")]
        public string OtherDetail { get; set; }
        [DisplayName("Is Billable")]
        public bool IsBillable { get; set; }

        [DisplayName("Projected Revenue")]
        public decimal ProjectedRevenue { get; set; }
        [DisplayName("Allocated Budget")]
        public decimal AllocatedBudget { get; set; }

        [DisplayName("Laptop / PC")]
        public bool HasLaptopPC { get; set; }
        [DisplayName("Laptop Details")]
        public string LaptopDetails { get; set; }
        [DisplayName("Headset")]
        public bool HasHeadset { get; set; }
        [DisplayName("Mouse")]
        public bool HasMouse { get; set; }
        [DisplayName("Email")]
        public bool HasEmail { get; set; }
        [DisplayName("Email Details")]
        public string EmailDetails { get; set; }
        [DisplayName("US DID")]
        public bool HasUSDID { get; set; }
        [DisplayName("Internet Access")]
        public bool HasInternetAccess { get; set; }
        [DisplayName("Internet Access Details")]
        public string InternetAccessDetails { get; set; }
        [DisplayName("Application Software Security Details")]
        public string ApplicationSoftwareSecurityDetails { get; set; }
        [DefaultValue("False")]
        public bool IsDeleted { get; set; }
        public string Status { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        [NotMapped]
        public IEnumerable<UserDepartmentVM> UserDepartments { get; set; }

        [NotMapped]
        public long[] SelectedDepartment { get; set; }
        [NotMapped]
        public decimal[] SelectedDepartmentPercentage { get; set; }
        [NotMapped]
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        public IEnumerable<UserAllocation> UserAllocatedDepartments { get; set; }
    }
}

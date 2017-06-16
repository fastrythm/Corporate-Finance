using CorporateAndFinance.Core.Helper.Structure;
using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserVM
    {
        
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        [StringLength(128)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(50)]
        public string EmployeeNumber { get; set; }

        [StringLength(50)]
        public string Mobile { get; set; }

        public string Designation { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Permissions")]
        public  List<UserAppPermissions> UserPermissions { get; set; }

        [Display(Name = "User Deparments")]
       
        public IEnumerable<UserDepartment> UserDepartments { get; set; } 

        [Display(Name = "Roles")]
        public IEnumerable<SelectListItem> RolesList { get; set; }
        [Required]
        public string[] SelectedRoles { get; set; }
        [Required]
        public long[] SelectedDepartment { get; set; }

        public string[] IsPrimaryDepartment { get; set; }

        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
    }
}

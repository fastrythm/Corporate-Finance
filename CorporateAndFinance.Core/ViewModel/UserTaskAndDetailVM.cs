using CorporateAndFinance.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.ViewModel
{
    public class UserTaskAndDetailVM
    {
        public UserTaskAndDetailVM()
        {
            Users = new List<ApplicationUser>();
        }

        public long UserTaskID { get; set; }
        [StringLength(50)]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [StringLength(25)]
        [Required]
        public string Type { get; set; }
        [Required]
        public int Priority { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}",
              ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; } = DateTime.Now;
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public  UserTaskDetail UserTaskDetails { get; set; } =  new UserTaskDetail();

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}

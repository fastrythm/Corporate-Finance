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
    public class UserTaskDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserTaskDetailID { get; set; }
        public long UserTaskID { get; set; }
        public Guid FromUserID { get; set; }
        [Required]
        [Display(Name ="Assign To User")]
        public Guid ToUserID { get; set; }
        public string Remarks { get; set; }
        [DefaultValue("true")]
        public bool IsActive { get; set; }

        [Required]
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public virtual UserTask UserTask { get; set; }
    
    }
}

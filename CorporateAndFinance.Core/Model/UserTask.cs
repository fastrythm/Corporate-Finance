using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Model
{
    public class UserTask
    {
        public UserTask()
        {
            UserTaskDetails = new HashSet<UserTaskDetail>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserTaskID { get; set; }
        [StringLength(50)]
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [StringLength(25)]
        public string Type { get; set; }
        public int Priority { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<UserTaskDetail> UserTaskDetails { get; set; }
    }
}

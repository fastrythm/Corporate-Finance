using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateAndFinance.Core.Model
{
    [Table("UserPermission")]
    public class UserPermission
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserID { get; set; }
        public long PermissionID { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
    }
}

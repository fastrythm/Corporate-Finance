namespace CorporateAndFinance.Core.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserCard")]
    public partial class UserCard
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserCard()
        {
            UserCardExpenses = new HashSet<UserCardExpense>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserCardID { get; set; }

        public Guid UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string CardType { get; set; }

        [Required]
        [StringLength(50)]
        public string CardNumber { get; set; }

        [StringLength(50)]
        public string CardMemberTitle { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime LastModified { get; set; }

      //  public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserCardExpense> UserCardExpenses { get; set; }
    }
}

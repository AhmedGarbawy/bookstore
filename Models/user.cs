namespace NEWS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("user")]
    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            news = new HashSet<news>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="*")]
        public string Name { get; set; }
        [Required(ErrorMessage ="*")]
        [StringLength(50)]
        [Remote("checkEmail","user",ErrorMessage = "This Email Found")]
        public string E_mail { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="*")]
        public string Password { get; set; }
        [Required]
       [DisplayName("ConfirmPassword")]
       [System.Web.Mvc.Compare("Password",ErrorMessage ="not matched")]
        [NotMapped]
        public string Confirm_password { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<news> news { get; set; }
    }
}

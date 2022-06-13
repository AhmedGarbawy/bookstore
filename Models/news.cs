namespace NEWS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        [StringLength(int.MaxValue)]
        [Required(ErrorMessage ="*")]
        [DisplayName("Title of News")]
        public string Tittle { get; set; }
        [StringLength(int.MaxValue)]
        [Required(ErrorMessage ="*")]
        public string Descr { get; set; }

        [StringLength(int.MaxValue)]
        [Required(ErrorMessage = "*")]
        public string pref { get; set; }
        [DisplayName("Date and Time of News")]
        
        public DateTime? date { get; set; }

        public string photo { get; set; }

        public int? user_id { get; set; }

        public int? category_id { get; set; }

        public virtual category category { get; set; }

        public virtual user user { get; set; }
    }
}

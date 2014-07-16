namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.organisation")]
    public partial class organisation
    {
        public organisation()
        {
            contacts = new HashSet<contact>();
            programs = new HashSet<program>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(45)]
        public string name { get; set; }

        [StringLength(45)]
        public string street { get; set; }

        [StringLength(45)]
        public string city { get; set; }

        [StringLength(45)]
        public string zip { get; set; }

        [StringLength(45)]
        public string country { get; set; }

        public bool active { get; set; }

        public int? phones_id { get; set; }

        public int? emails_id { get; set; }

        public virtual ICollection<contact> contacts { get; set; }

        public virtual email email { get; set; }

        public virtual phone phone { get; set; }

        public virtual ICollection<program> programs { get; set; }
    }
}

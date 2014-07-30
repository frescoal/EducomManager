namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.responses")]
    public partial class Respons
    {
        public int id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [StringLength(65535)]
        public string response { get; set; }

        public int requests_id { get; set; }

        public int questions_id { get; set; }

        public virtual Question question { get; set; }

        public virtual Request request { get; set; }
    }
}

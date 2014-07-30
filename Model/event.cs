namespace PrototypeEDUCOM.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EducomDb.events")]
    public partial class Event
    {
        public int id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Column(TypeName = "text")]
        [StringLength(65535)]
        public string comment { get; set; }

        public int event_types_id { get; set; }

        public int requests_id { get; set; }

        public virtual EventTypes event_types { get; set; }

        public virtual Request request { get; set; }
        public override string ToString()
        {
            return this.event_types.name;
        }

    }
}

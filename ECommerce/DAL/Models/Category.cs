namespace DAL.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [StringLength(50)]
        public string description { get; set; }

        public byte[] image { get; set; }

        [StringLength(50)]
        public string status { get; set; }

        [StringLength(50)]
        public string createdBy { get; set; }

        [StringLength(50)]
        public string createdDate { get; set; }

        [StringLength(50)]
        public string lastModifiedBy { get; set; }

        [StringLength(50)]
        public string lastModifiedDate { get; set; }
    }
}

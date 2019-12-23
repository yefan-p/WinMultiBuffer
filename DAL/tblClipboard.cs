namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblClipboard
    {
        public int idUser { get; set; }

        public int intCopyKeyCode { get; set; }

        public int intPasteKeyCode { get; set; }

        [StringLength(50)]
        public string nvcName { get; set; }

        public string nvcValue { get; set; }

        public int id { get; set; }

        public virtual tblUser tblUser { get; set; }
    }
}

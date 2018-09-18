namespace WebMVC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Picture")]
    public partial class Picture
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Path { get; set; }

        [Required]
        public string PictureDescription { get; set; }

        public bool IsSnail { get; set; }

        public byte[] BinaryPic { get; set; }

        public int Id { get; set; }
    }
}

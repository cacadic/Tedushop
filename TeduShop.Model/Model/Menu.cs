﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Model.Model
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(256)]
        public string URL { get; set; }

        public int? DisplayOrder { get; set; } //Cho phép null
        [Required]
        public int GroupID { get; set; }
        [ForeignKey("GroupID")] //Khóa ngoại trỏ đến table GroupID
        public virtual MenuGroup MenuGroup { get; set; }
        [MaxLength(10)]
        public string Target { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}

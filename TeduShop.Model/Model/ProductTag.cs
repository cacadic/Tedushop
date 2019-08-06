using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeduShop.Model.Model
{
    [Table("ProductTags")]
    public class ProductTag
    {
        [Key]
        [MaxLength(50)]
        [Column(TypeName = "varchar", Order = 1)]
        public string TagID { get; set; }

        [Key]
        [Column(Order = 2)]
        public int ProductID { get; set; }

        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }

        [ForeignKey("TagID")]
        public virtual Tag Tag { get; set; }
    }
}
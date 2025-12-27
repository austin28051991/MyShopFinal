using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedOn {  get; set; }
        public string CreatedBy { get; set; }
    }
}

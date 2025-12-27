using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace MyShop.ViewModels
{
    public class CategoryViewModel
    {
        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
        [Required]
        [DisplayName("Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }

    }
}

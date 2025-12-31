using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace MyShop.ViewModels
{
    public class SubCategoryViewModel
    {
        [Required]
        [DisplayName("SubCategory Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("SubCategory Description")]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public List<SelectListItem>? categories {  get; set; }
    }
}

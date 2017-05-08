using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models.ViewModels
{
    public class ProductVM
    {
        public int ProductID { get; set; }

        [Required]
        public string ProductName { get; set; }

        public bool? Active { get; set; }

        [Required]
        [Range(50, 100)]
        public decimal? Price { get; set; }

        [Required]
        [Range(100, 200)]
        public decimal? Stock { get; set; }
    }
}
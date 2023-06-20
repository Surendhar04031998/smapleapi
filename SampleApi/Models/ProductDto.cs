using System.ComponentModel.DataAnnotations;

namespace SampleApi.Models
{
    public class ProductDto
    {
        

        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Brand { get; set; } = "";
        [Required]
        public string Category { get; set; } = "";
        [Required]
        public string price { get; set; }
        [Required]
        public string Discription { get; set; } = "";
        
    }
}

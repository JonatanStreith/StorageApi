using System.ComponentModel.DataAnnotations;

namespace StorageApi.Models
{
    public class ProductCreateDto
    {
        [Required(ErrorMessage ="A name is required for the product.")]
        [MaxLength(50)]
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Category { get; set; }
        public string? Shelf { get; set; }
        public int Count { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }

    }
}

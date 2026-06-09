using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageApi.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        public int Price { get; set; }
        public string? Category { get; set; }
        public string? Shelf { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
    }
}

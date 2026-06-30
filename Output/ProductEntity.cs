using System.ComponentModel.DataAnnotations;

namespace <Your Namespace>.Entities;

public class ProductEntity
{

    [Key]
        public int Id { get; set; }


    [Required]
    public string Name { get; set; }


    public decimal Price { get; set; }


    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
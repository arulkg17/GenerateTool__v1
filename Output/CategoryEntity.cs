using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Entities;

public class CategoryEntity
{
        [Key]

    public int Id { get; set; }

        [Required]

    public string Code { get; set; }

        [Required]

    public string Name { get; set; }


    public string Description { get; set; }


    public bool IsActive { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
using System.ComponentModel.DataAnnotations;

namespace InvoiceManagement.Entities;

public class CategoryEntity
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Code { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    [Required]
    public bool IsActive { get; set; }

    [Required]
    public string CreatedBy { get; set; }

    public DateTime CreatedDate { get; set; }

    [Required]
    public string UpdatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
}
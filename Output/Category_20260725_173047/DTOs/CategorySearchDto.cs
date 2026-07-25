using System.ComponentModel.DataAnnotations;
namespace InvoiceManagement.Dtos;
public class CategorySearchDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

}
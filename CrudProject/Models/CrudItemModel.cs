using System.ComponentModel.DataAnnotations;

public class CrudItem
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime LastEdit { get; set; }
}
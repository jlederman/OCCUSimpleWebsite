using System.ComponentModel.DataAnnotations;

public class CrudItem
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string Status { get; set; }
    public DateTime LastEdit { get; set; }
    public string? Notes { get; set; }
}

// Not a huge fan of multiple classes in a single file, but this smells better than dumping it in the controller.
// In larger applications this gets messy. 
public class UpdateNotesModel
{
    public int Id { get; set; }
    public string? Notes { get; set; }
}

public enum CrudItemStatus
{
    Fail,
    Warn,
    Pass
}
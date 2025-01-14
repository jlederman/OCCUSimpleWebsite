using Microsoft.AspNetCore.Mvc.Rendering;

public class CrudItemComparisonViewModel
{
    public List<SelectListItem> CrudItemOptions { get; set; } = new();
    public int? CrudItemId1 { get; set; }
    public int? CrudItemId2 { get; set; }
    public CrudItem CrudItem1 { get; set; }
    public CrudItem CrudItem2 { get; set; }
}
public class StatusListModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
}

public enum Status
{
    Fail,
    Warn,
    Pass
}
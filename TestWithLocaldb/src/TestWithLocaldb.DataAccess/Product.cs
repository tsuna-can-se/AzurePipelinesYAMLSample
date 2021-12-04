namespace TestWithLocaldb.DataAccess;

public partial class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Publisher { get; set; } = string.Empty;
}

namespace G12_DataImporter;

public sealed class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public decimal Price { get; set; }
    public int CategoryID { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime UpdateDate { get; set; }
}
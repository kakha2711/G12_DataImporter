namespace G12_DataImporter;

public sealed class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string CategoryCode { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreateDate { get; set; } = DateTime.Now;
    public DateTime UpdateDate { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
} 
using System.Security.Cryptography.X509Certificates;

namespace G12_DataImporter;

public static class FileDataLoader
{
	private const char Separator = '\t';
    
    

    public static List<Category> GetCategories(string filePath)
    {
        List<Category> categories = new List<Category>();
        using (StreamReader streamReader = new StreamReader(filePath))
        {
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();

                if (string.IsNullOrEmpty(line)) continue;

                if (ValidateFileData(line))
                {
                    if (!categories.Any(c => (c.CategoryName == line.Split(Separator)[0] || c.CategoryCode == line.Split(Separator)[1]))) 
                    categories.Add(new Category { CategoryCode = line.Split(Separator)[1], CategoryName = line.Split(Separator)[0] });

                    int index = categories.Count - 1;
                    int CategoryIndex = categories.FindIndex(c=>c.CategoryName== line.Split(Separator)[0] && c.CategoryCode== line.Split(Separator)[1]);

                    categories[index].Products.Add(new Product
                                                        {
                                                            ProductName = line.Split(Separator)[2],
                                                            ProductCode = line.Split(Separator)[3],
                                                            Price = Convert.ToDecimal(line.Split(Separator)[4]),
                                                            CategoryID = CategoryIndex
                                                        });
                }
            }
        }
        return categories;
    }

    public static List<Product> GetProducts(Product product)
    {
        List<Product> products = new List<Product>();

        products.Add(new Product
                            {
                                ProductName= product.ProductName,
                                ProductCode= product.ProductCode,
                                Price=product.Price,
                                CategoryID=product.CategoryID
                            });

        return products;
    }

    public static bool ValidateFileData(string strings)
    {

        if (string.IsNullOrEmpty(strings)) return false;

        if (strings.Split(Separator).Length > 5 || strings.Split(Separator).Length < 5) return false;
        if (string.IsNullOrEmpty(strings.Split(Separator)[0])
            || string.IsNullOrEmpty(strings.Split(Separator)[1])
            || string.IsNullOrEmpty(strings.Split(Separator)[2])
            || string.IsNullOrEmpty(strings.Split(Separator)[3])
            || string.IsNullOrEmpty(strings.Split(Separator)[4]))
            return false;

        return true;
    }
}
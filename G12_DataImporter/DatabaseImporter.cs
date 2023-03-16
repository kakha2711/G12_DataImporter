using System.Data;
using System.Data.SqlClient;

namespace G12_DataImporter;

public static class DatabaseImporter
{
	private const string ConnectionString = @"server=DESKTOP-610ITHU; database=G12_Catalogue; integrated security=true; TrustServerCertificate=True;";

    public static void ImportCatalogue(IEnumerable<Category> catalogue)
	{
        Execute(ConnectionString, catalogue);
    }

    private static void Execute(string connectionString, IEnumerable<Category> catalogue)
    {
        foreach (var value in catalogue)
        {
            if (SelectDataDB(connectionString, value))
            {
                using (SqlConnection connection = new(connectionString))
                {
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;

                    command.CommandText =
                           "insert into Categories (CategoryName, CategoryCode)values (" + "'" + value.CategoryName + "'" + ", " + "'" + value.CategoryCode + "'" + ")" + "SELECT CAST(scope_identity() AS int)";

                    try
                    {
                        connection.Open();
                        int index = (int)command.ExecuteScalar();

                        foreach (var category in value.Products)
                        {
                            var tt = value.Products;
                        }
                        //command.CommandText = "insert into Product(CategoryName, CategoryCode, Price, CategoryID) values (" + "'" + value.Products

                        Console.WriteLine(index);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    connection.Close();
                }

            }
        }
        Console.WriteLine("Execute completed");
    }



    private static bool SelectDataDB(string connectionString, Category catalogue)
    {
        Category category = new Category();

        using (SqlConnection connection = new(connectionString))
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.Text;

            command.CommandText = "select CategoryName, CategoryCode from Categories where CategoryName like "
                                            + "'" + catalogue.CategoryName.ToString() + "'";

            try
            {
                connection.Open();
                SqlDataReader count = command.ExecuteReader();

                while (count.Read())
                {
                    category.CategoryName = count["CategoryName"].ToString();
                    category.CategoryCode = count[2].ToString();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        if (category == null ||
            category.CategoryName == "" &&
            category.CategoryName == null &&
            category.CategoryName == string.Empty &&
            category.CategoryCode == "" &&
            category.CategoryCode == null &&
            category.CategoryCode == string.Empty
            )
            return true;

        return false;
    }
}
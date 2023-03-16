namespace G12_DataImporter
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string filePath = Console.ReadLine();
			var catalogue = FileDataLoader.GetCategories(filePath);
			DatabaseImporter.ImportCatalogue(catalogue);
		}
	}
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarProgram
{
    internal class Product
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public Product(int id, string name, int price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }
        public static List<Product> LoadFromCsv(string filePath)
        {
            var products = new List<Product>();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"CSV file not found: {filePath}");
            }

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                var id = int.Parse(parts[0].Trim());
                var name = parts[1].Trim();
                var price = int.Parse(parts[2].Trim());

                products.Add(new Product(id, name, price));
            }
            return products;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raktarProgram
{
    internal class Sale
    {
        public int id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public int amount { get; set; }
        public Sale(int id, string name, string date, int amount)
        {
            this.id = id;
            this.name = name;
            this.date = date;
            this.amount = amount;
        }
        public static List<Sale> LoadFromCsv(string filePath)
        {
            var sales = new List<Sale>();
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"CSV file not found: {filePath}");
            }

            var lines = File.ReadAllLines(filePath).Skip(1);
            foreach(var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(',');
                var id = int.Parse(parts[0].Trim());
                var name = parts[1].Trim();
                var date = parts[2].Trim();
                var amount = int.Parse(parts[3].Trim());

                sales.Add(new Sale(id, name, date, amount));
            }
            return sales;
        }
    }
}

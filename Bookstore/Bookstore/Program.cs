using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using static Bookstore.Constants;

namespace Bookstore
{
    public class Book
    {
        public string id { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public double price { get; set; }
        public string publish_date { get; set; }
        public string description { get; set; }
    }
    class Program
    {
        public static void Main()
        {
            List<Book> books = new List<Book>();
            string json = GetStringFromJson(Constants.filename);
            books = ConvertJsonToList(json);
            RoundUpPrices(books);
            DeleteSaturdayBooks(books);
            DeleteAuthorNamedPeter(books);
            SortListByName(books);
            SaveListToCsv(books, Constants.outputType);
        }

        private static void SaveListToCsv(List<Book> books, string outputType)
        {
            if (outputType.Equals("csv"))
            {
                using var writer = new StreamWriter("output.csv");
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteHeader<Book>();
                csv.NextRecord();
                foreach (var record in books)
                {
                    csv.WriteRecord(record);
                    csv.NextRecord();
                }
            }
            // Add more DB types here...
        }

        private static void SortListByName(List<Book> books)
        {
            books.OrderBy(book => book.title);
        }

        private static bool IsSaturday(Book book)
        {
            DateTime dt = (DateTime)Convert.ChangeType(book.publish_date, typeof(DateTime));
            return (dt.DayOfWeek == DayOfWeek.Saturday);
        }

        private static void DeleteAuthorNamedPeter(List<Book> books)
        {
            books.RemoveAll(book => book.author.Contains("Peter"));
        }

        private static void DeleteSaturdayBooks(List<Book> books)
        {
            books.RemoveAll(IsSaturday);
        }

        private static string GetStringFromJson(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                return r.ReadToEnd();
            }
        }

        private static List<Book> ConvertJsonToList(string json)
        {
            return JsonConvert.DeserializeObject<List<Book>>(json);
        }

        private static void RoundUpPrices(List<Book> books)
        {
            books.ForEach(book => book.price = Math.Ceiling(book.price));
        }
    }
}
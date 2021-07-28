using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Bookstore
{
    class DAL
    {
        private string GetStringFromJson(string filename)
        {
            using StreamReader r = new StreamReader(filename);
            return r.ReadToEnd();
        }

        private List<Book> ConvertJsonToList(string json)
        {
            return JsonConvert.DeserializeObject<List<Book>>(json);
        }

        public List<Book> CreateListFromData(string inputType)
        {
            if (inputType.Equals("json"))
            {
                return ConvertJsonToList(GetStringFromJson(Constants.inputFilename));
            }
            // Add other input data types here...
            return new List<Book>();
        }

        public void FilterListByAuthorAndDay(List<Book> books, string author, DayOfWeek day)
        {
            books.RemoveAll(book => isAuthor(book, author) || isPublishedDay(book, day));
        }

        private bool isAuthor(Book book, string name)
        {
            return book.author.Contains(name);
        }

        private bool isPublishedDay(Book book, DayOfWeek day)
        {
                DateTime dt = (DateTime)Convert.ChangeType(book.publish_date, typeof(DateTime));
                return (dt.DayOfWeek == day);
        }

        public void RoundUpPrices(List<Book> books)
        {
            books.ForEach(book => book.price = Math.Ceiling(book.price));
        }

        public void SortListByName(List<Book> books)
        {
            TitleComparer titleComparer = new TitleComparer();
            books.Sort(titleComparer);
        }

        private class TitleComparer : IComparer, IComparer<Book>
        {
            public int Compare(Book a, Book b)
            {
                return a.title.CompareTo(b.title);
            }
            int IComparer.Compare(Object a, Object b)
            {
                return Compare((Book)a, (Book)b);
            }
        }

        public void SaveListToDB(List<Book> books, string outputType)
        {
            if (outputType.Equals("csv"))
            {
                using (var writer = new StreamWriter(Constants.outputFilename))
                using (var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    csv.WriteRecords(books);
                }
            }
            // Add other output types here...
        }
    }
}

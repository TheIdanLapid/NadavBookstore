using CsvHelper;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Bookstore
{
    public class BL
    {
        public void FilterBooksByAuthorAndDay(IOrderedEnumerable<Book> books, string author, DayOfWeek day)
        {
            books.ToList().RemoveAll(book => IsAuthor(book, author) || IsPublishedDay(book, day));
        }

        private bool IsAuthor(Book book, string name)
        {
            return book.author.Contains(name);
        }

        private bool IsPublishedDay(Book book, DayOfWeek day)
        {
            DateTime dt = (DateTime)Convert.ChangeType(book.publish_date, typeof(DateTime));
            return (dt.DayOfWeek == day);
        }

        public void RoundUpPrices(IOrderedEnumerable<Book> books)
        {
            books.ToList().ForEach(book => book.price = Math.Ceiling(book.price));
        }

        public void SortBooksByName(IOrderedEnumerable<Book> books)
        {
            books = books.OrderBy(book => book.title);
        }
    }
}

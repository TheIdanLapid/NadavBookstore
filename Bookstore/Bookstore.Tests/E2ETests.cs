using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bookstore.Tests
{
    [TestClass]
    public class E2ETests
    {
        private readonly DAL dal = new DAL();
        [TestMethod]
        public void Csv_File_Valid()
        {
            DAL dal = new DAL();
            BL bl = new BL();

            // Create an ordered collection of books from the JSON file
            IOrderedEnumerable<Book> books = dal.CreateIEnumerableFromJsonData().OrderBy(book => book.title);

            bl.FilterBooksByAuthorAndDay(books, "Peter", DayOfWeek.Saturday);
            bl.RoundUpPrices(books);
            bl.SortBooksByName(books);

            // Write the processed collection to the DB (in this case, a CSV file)
            dal.SaveIEnumerableToDB(books, "csv");
            Assert.IsTrue(File.Exists("output.csv"));
        }
    }
}

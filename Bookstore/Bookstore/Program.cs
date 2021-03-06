using System;
using System.Diagnostics;
using System.Linq;

namespace Bookstore
{
    class Program
    {
        public static void Main()
        {
            DAL dal = new DAL();
            BL bl = new BL();

            // Create an ordered collection of books from the JSON file
            IOrderedEnumerable<Book> books = dal.CreateIEnumerableFromJsonData().OrderBy(book => book.title);

            bl.FilterBooksByAuthorAndDay(books, "Peter", DayOfWeek.Saturday);
            bl.RoundUpPrices(books);
            bl.SortBooksByName(books);
            
            // Write the processed collection to the DB (in this case, a CSV file)
            dal.SaveIEnumerableToDB(books, Constants.outputType);
        }
    }
}
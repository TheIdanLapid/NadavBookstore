using System;
using System.Collections.Generic;
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

            IOrderedEnumerable<Book> books = dal.CreateIEnumerableFromJsonData().OrderBy(book => book.title);

            bl.FilterBooksByAuthorAndDay(books, "Peter", DayOfWeek.Saturday);
            bl.RoundUpPrices(books);
            bl.SortBooksByName(books);
            
            dal.SaveIEnumerableToDB(books, Constants.outputType);

            Process.Start("notepad.exe", Constants.outputFilename);
        }
    }
}
using System;
using System.Collections.Generic;

namespace Bookstore
{
    class Program
    {
        public static void Main()
        {
            DAL dal = new DAL();

            List<Book> books = dal.CreateListFromData(Constants.inputType);
            dal.FilterListByAuthorAndDay(books, "Peter", DayOfWeek.Saturday);
            dal.RoundUpPrices(books);
            dal.SortListByName(books);
            dal.SaveListToDB(books, Constants.outputType);
        }
    }
}
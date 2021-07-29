using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bookstore.Tests
{
    [TestClass]
    public class DALTests
    {
        private readonly DAL dal = new DAL();
        [TestMethod]
        public void Json_Is_Valid()
        {
            try
            {
                dal.CreateIEnumerableFromJsonData();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                Console.WriteLine("Something is wrong with the JSON input file.");
                throw;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Json_Is_Not_Empty()
        {
            try
            {
                IEnumerable<Book> books = dal.CreateIEnumerableFromJsonData();
                Assert.IsTrue(books != null || books.Any());
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                throw;
            }
        }
    }
}

using CsvHelper;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Bookstore
{
    public class DAL
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

        public IEnumerable<Book> CreateIEnumerableFromJsonData()
        {
            return ConvertJsonToList(GetStringFromJson(Constants.inputFilename)).AsEnumerable();
        }

        public void SaveIEnumerableToDB(IOrderedEnumerable<Book> books, string outputType)
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

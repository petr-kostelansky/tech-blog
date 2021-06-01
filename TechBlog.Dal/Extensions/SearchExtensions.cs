using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TechBlog.Dal.Extensions
{
    internal static class SearchExtension
    {
        public static string RemoveChars(this string source)
        {
            return Regex.Replace(source, "[^a-zA-Z0-9 --&#]+", String.Empty);
        }

        public static DataTable ToSearchWords(this string source)
        {
            string[] words = source.Split(' ');

            DataTable searchWords = new DataTable();
            // Adding Columns    
            DataColumn columnWord = new DataColumn();
            columnWord.ColumnName = "Word";
            columnWord.DataType = typeof(string);
            searchWords.Columns.Add(columnWord);

            foreach (string word in words)
            {
                DataRow row = searchWords.NewRow();
                row["Word"] = word;
                searchWords.Rows.Add(row);
            }

            return searchWords;
        }
    }
}

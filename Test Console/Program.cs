using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Test_Console
{

    public static class TableParser
    {
        public static string ToStringTable<T>(
          this IEnumerable<T> values,
          string[] columnHeaders,
          params Func<T, object>[] valueSelectors)
        {
            return ToStringTable(values.ToArray(), columnHeaders, valueSelectors);
        }

        public static string ToStringTable<T>(
          this T[] values,
          string[] columnHeaders,
          params Func<T, object>[] valueSelectors)
        {
            Debug.Assert(columnHeaders.Length == valueSelectors.Length);

            var arrValues = new string[values.Length + 1, valueSelectors.Length];

            // Fill headers
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                arrValues[0, colIndex] = columnHeaders[colIndex];
            }

            // Fill table rows
            for (int rowIndex = 1; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    arrValues[rowIndex, colIndex] = valueSelectors[colIndex]
                      .Invoke(values[rowIndex - 1]).ToString();
                }
            }

            return ToStringTable(arrValues);
        }

        public static string ToStringTable(this string[,] arrValues)
        {
            int[] maxColumnsWidth = GetMaxColumnsWidth(arrValues);
            var headerSpliter = new string('-', maxColumnsWidth.Sum(i => i + 3) - 1);

            var sb = new StringBuilder();
            for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
                {
                    // Print cell
                    string cell = arrValues[rowIndex, colIndex];
                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    sb.Append(" | ");
                    sb.Append(cell);
                }

                // Print end of line
                sb.Append(" | ");
                sb.AppendLine();

                // Print splitter
                if (rowIndex == 0)
                {
                    sb.AppendFormat(" |{0}| ", headerSpliter);
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        private static int[] GetMaxColumnsWidth(string[,] arrValues)
        {
            var maxColumnsWidth = new int[arrValues.GetLength(1)];
            for (int colIndex = 0; colIndex < arrValues.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < arrValues.GetLength(0); rowIndex++)
                {
                    int newLength = arrValues[rowIndex, colIndex].Length;
                    int oldLength = maxColumnsWidth[colIndex];

                    if (newLength > oldLength)
                    {
                        maxColumnsWidth[colIndex] = newLength;
                    }
                }
            }

            return maxColumnsWidth;
        }
    }
    class Program
    {

      

        static void Main(string[] args)
        {
            //  Console.WriteLine("123".IsNumeric());

            //  Console.WriteLine(ReverseClass.ReverseWords("Hello there guys !"));

            //  Console.WriteLine("Hi there guys".ReverseText());


            


            IEnumerable<Tuple<string, string, string>> authors =
    new[]
    {
      Tuple.Create("1", "Isaac", "Asimov"),
      Tuple.Create("2", "Robert", "Heinlein"),
      Tuple.Create("3", "", "Herbert"),
      Tuple.Create("4", "Aldous", "Huxley"),
    };

            Console.WriteLine(authors.ToStringTable(
              new[] { "Id", "First Name", "Surname" },
              a => a.Item1, a => a.Item2, a => a.Item3));


        }
    }
}

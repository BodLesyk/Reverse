using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Console
{
    static class ReverseClass
    {

        public static string ReverseWords(string str)
        {
            var reversedWords = string.Join(" ",
       str.Split(' ')
          .Select(x => new String(x.Reverse().ToArray())));

            return reversedWords;
        }

        public static string ReverseText(this string text)
        {
            return new string(text.ToCharArray().Reverse().ToArray());
        }

        
    }

}

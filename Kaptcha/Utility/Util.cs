using System;
using System.Collections.Generic;
using System.Text;

namespace Kaptcha.Utility
{
    internal static class Util
    {
        internal static string PersianToEnglish(string persianStr)
        {
            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['0'] = '۰',
                ['1'] = '۱',
                ['2'] = '۲',
                ['3'] = '۳',
                ['4'] = '۴',
                ['5'] = '۵',
                ['6'] = '۶',
                ['7'] = '۷',
                ['8'] = '۸',
                ['9'] = '۹'
            };
            foreach (var item in persianStr)
            {
                persianStr = persianStr.Replace(item, LettersDictionary[item]);
            }
            return persianStr;
        }
    }
}

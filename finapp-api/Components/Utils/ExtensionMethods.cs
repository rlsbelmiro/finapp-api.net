using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinAppApi.Components.Utils
{
    public static class ExtensionMethods
    {
        public static String OnlyNumbers(this String str)
        {
            str = new string(str.Where(char.IsDigit).ToArray());
            return str;
        }
    }
}

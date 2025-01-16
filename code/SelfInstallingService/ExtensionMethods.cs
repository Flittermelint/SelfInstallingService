using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static bool EqualsIC(this string obj, string Text)
        {
            return obj.Equals(Text, StringComparison.OrdinalIgnoreCase);
        }
        public static bool StartsWithIC(this string obj, string Text)
        {
            return obj.StartsWith(Text, StringComparison.OrdinalIgnoreCase);
        }
    }
}
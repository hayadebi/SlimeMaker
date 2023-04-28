using System;
using System.Linq;

public static class StringExt
{
    public static string GetFirstLine(this string self)
    {
        var separator = new[] { Environment.NewLine };

        return self
            .Split(separator, StringSplitOptions.None)
            .FirstOrDefault()
        ;
    }
}
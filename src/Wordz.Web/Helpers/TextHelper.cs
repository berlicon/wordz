using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Wordz.Web.Helpers
{
    public static class TextHelper
    {
        public static NumberFormatInfo _nfi = new NumberFormatInfo
                                                 {NumberDecimalSeparator = ".", CurrencyDecimalSeparator = "."};

        public static string GetDecimalCaptionJs(this decimal value)
        {
            return value.ToString(_nfi);
        }

        public static string GetFloatCaptionJs(this float value)
        {
            return value.ToString(_nfi);
        }

        public static string GetDoubleCaptionJs(this double value)
        {
            return value.ToString(_nfi);
        }

        public static string ReturnEmptyIfNull(this string source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            return source;
        }

        public static string ReturnStringIfNull(this string source, string replacement)
        {
            if (source == null)
            {
                return replacement;
            }
            return source;
        }

        public static string HtmlEncode(this string source)
        {
            return WebUtility.HtmlEncode(source);
        }

        public static string ReplaceScriptTags(this string source)
        {
            var regex = new Regex("<[/]?script>");
            return regex.Replace(source, string.Empty);
        }

        public static string GetLinksFromStrings(IEnumerable<string> input)
        {
            return string.Join(" ", input.Select(str => "<a href='" + str + "'>" + str + "</a>"));
        }
        
        public static string GetLinksFromString(string input)
        {
            var strings = Regex.Split(input, @"\s");
            return GetLinksFromStrings(strings);
        }
    }
}
using Humanizer;
using NickBuhro.Translit;
using System.Text.RegularExpressions;

namespace AdminPanel.Helpers;

public static class UrlHelper
{
    public static string CreateProductUrl(string productName, int productCode)
    {
        var url = TransformName(productName);

        url = Transliteration.CyrillicToLatin(url)
            .Replace("`", "")
            .Replace("'", "");

        if (url.Last() != '-') url += "-";
        url += $"{productCode}";

        return url;
    }

    public static string CreateCategoryUrl(string categoryName)
    {
        var url = TransformName(categoryName);

        url = Transliteration.CyrillicToLatin(url)
            .Replace("`", "")
            .Replace("'", "");

        return url;
    }


    private static string TransformName(string name)
    {
        var symbolsToReplaceToDash = new List<string> { " ", ".", ",", "/", "\\", "(", ")", "[", "]", "{", "}", "?", "!" };

        var result = name
            .ToLower()
            .Replace("и", "ы")
            .Replace("ґ", "г")
            .Replace("є", "е")
            .Replace("і", "и")
            .Replace("ї", "йи")
            .Dasherize();

        foreach (var symbol in symbolsToReplaceToDash)
        {
            result = result.Replace(symbol, "-");
        }

        result = Regex.Replace(result, @"(-)\1+", "$1"); // remove duplicate dashes
        return result;
    }
}
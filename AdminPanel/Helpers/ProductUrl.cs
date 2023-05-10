using Humanizer;
using NickBuhro.Translit;
using System.Text;

namespace AdminPanel.Helpers
{
    public static class ProductUrl
    {
        public static string Create(string productName, int productCode)
        {
            string url = productName
                    .ToLower()
                    .Replace(" ", "-")
                    .Replace(".", "-")
                    .Replace("/", "-")
                    .Replace("\\", "-")
                    .Replace("(", "-")
                    .Replace(")", "-")
                    .Replace("ґ", "г")
                    .Replace("є", "е")
                    .Replace("і", "и")
                    .Replace("ї", "йи")
                    .Replace("[", "-")
                    .Replace("]", "-")
                    .Replace("{", "-")
                    .Replace("}", "-")
                    .Replace("?", "-")
                    .Replace("!", "-")
                    .Replace("----", "-")
                    .Replace("---", "-")
                    .Replace("--", "-")
                    .Dasherize();
            url = Transliteration.CyrillicToLatin(url)
                .Replace("`", "");

            if (url.Last() != '-') url += "-";
            url += $"{productCode}";

            return url;
        }
    }
}

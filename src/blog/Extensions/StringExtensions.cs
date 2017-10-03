using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace blog
{
    public static class StringExtensions
    {
        public static string Slugify(this string text, int maxLength = 50)
        {
            string slug = text.RemoveDiacritics();

            slug = slug.Replace(" ", "-");

            slug = new Regex(@"([^A-Za-z0-9-])")
                .Replace(slug, "");

            slug = new Regex("-{2,}")
                .Replace(slug, "-");

            slug = slug.Truncate(maxLength);

            return slug.TrimEnd('-');
        }

        public static string RemoveDiacritics(this string text)
        {
            var s = new string(text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());
    
            return s.Normalize(NormalizationForm.FormC);
        }

        public static string Truncate(this string text, int maxLength)
        {
            if(text.Length <= maxLength)
            {
                return text;
            }
            else
            {
                return text.Substring(0, maxLength);
            }
        }
    }
}

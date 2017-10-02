using System.Text.RegularExpressions;

namespace blog
{
    public static class StringExtensions
    {
        public static string Slugify(this string val, int maxLength)
        {
            string spaceless = val.Replace(" ", "-");

            string slug = new Regex("([^A-Za-z0-9-])")
                .Replace(spaceless, "");

            if(slug.Length > maxLength)
            {
                return slug;
            }
            else
            {
                return slug.Substring(0, maxLength);
            }
        }
    }
}
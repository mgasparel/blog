using System;
using Xunit;
using blog.Services;
using blog;

namespace blog.UnitTests
{
    public class StringExtensions
    {
        [Fact(DisplayName = "When Title is longer than max slug length, truncate slug")]
        public void WhenTitleIsLongerThanMaxSlugLength_TruncateSlug()
        {
            string title = "Tougher Colder Killer (feat. Killer Mike & Despot)";

            string slug = title.Slugify(maxLength: 40);

            Assert.Equal("Tougher-Colder-Killer-feat-Killer-Mike-D", slug);
        }

        [Fact(DisplayName = "Replace all spaces with dashes")]
        public void ReplaceAllSpacesWithDashes()
        {
            string title = "The Jig Is Up";

            string slug = title.Slugify();

            Assert.Equal("The-Jig-Is-Up", slug);
        }

        [Fact(DisplayName = "Ignore Invalid Characters and Replace Spaces with Dashes")]
        public void IgnoreInvalidCharactersAndReplaceSpacesWithDashes()
        {
            string title = "$4 Vic / Nothing but Me and You (Ftl)";

            string slug = title.Slugify();

            Assert.Equal("4-Vic-Nothing-but-Me-and-You-Ftl", slug);
        }

        [Fact(DisplayName = "When Truncated Slug Ends With Dash Then Drop Trailing Dash")]
        public void WhenTruncatedSlugEndWithDash_ThenDropTrailingDash()
        {
            string title = "For My Upstairs Neighbor (Mums the Word)";

            string slug = title.Slugify(maxLength: 34);

            Assert.Equal("For-My-Upstairs-Neighbor-Mums-the", slug);
        }

        [Fact(DisplayName = "Strip Diacritic Characters from Slug")]
        public void EnsureDiacriticsAreIgnored()
        {
            string title = "Bruleé";

            string slug = title.Slugify();

            Assert.Equal("Brulee", slug);
        }
    }
}

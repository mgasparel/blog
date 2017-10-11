using System;
using Xunit;
using blog.Services;
using blog;
using Microsoft.EntityFrameworkCore;
using blog.Data;
using blog.Models;
using System.Threading.Tasks;
using System.Linq;
using Xunit.Abstractions;

namespace blog.UnitTests
{
    public class GetPostsQuery
    {
        private readonly ITestOutputHelper output;

        public GetPostsQuery(ITestOutputHelper output)
        {
            this.output = output;
        }

        private ApplicationDbContext GetTestDb(string databaseName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                context.Posts.Add(new Post { Title = "One", Published = new DateTime(2017, 1, 31) });
                context.Posts.Add(new Post { Title = "Two", Published = new DateTime(2017, 1, 30) });
                context.Posts.Add(new Post { Title = "Three", Published = new DateTime(2017, 1, 29) });
                context.Posts.Add(new Post { Title = "Four", Published = new DateTime(2017, 1, 28) });
                context.Posts.Add(new Post { Title = "Five", Published = new DateTime(2017, 1, 27) });
                context.Posts.Add(new Post { Title = "Six", Published = new DateTime(2017, 1, 26) });
                context.Posts.Add(new Post { Title = "Seven", Published = new DateTime(2017, 1, 25) });
                context.Posts.Add(new Post { Title = "Eight", Published = new DateTime(2017, 1, 24) });
                context.Posts.Add(new Post { Title = "Nine", Published = new DateTime(2017, 1, 23) });
                context.Posts.Add(new Post { Title = "Ten", Published = new DateTime(2017, 1, 22) });
                context.SaveChanges();
            }

            return new ApplicationDbContext(options);
        }

        [Theory(DisplayName = "Negative pages should return first page")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public async Task NegativePages_ShouldReturn_FirstPage(int page)
        {
            var db = GetTestDb("negative_pages_" + page);

            var query = new blog.Data.GetPostsQuery(db);

            var results = await query.ExecuteAsync(page, pageSize: 5);

            Assert.Collection<Post>(results, 
                x => Assert.Equal("One", x.Title),
                x => Assert.Equal("Two", x.Title),
                x => Assert.Equal("Three", x.Title),
                x => Assert.Equal("Four", x.Title),
                x => Assert.Equal("Five", x.Title)
            );
        }

        [Fact(DisplayName = "First Page should return first page")]
        public async Task FirstPage_ShouldReturn_FirstPage()
        {
            var db = GetTestDb("first_page");

            var query = new blog.Data.GetPostsQuery(db);

            var results = await query.ExecuteAsync(page: 1, pageSize: 5);

            Assert.Collection<Post>(results, 
                x => Assert.Equal("One", x.Title),
                x => Assert.Equal("Two", x.Title),
                x => Assert.Equal("Three", x.Title),
                x => Assert.Equal("Four", x.Title),
                x => Assert.Equal("Five", x.Title)
            );
        }

        [Fact(DisplayName = "Last Page can return fewer items than the page size")]
        public async Task LastPage_CanReturn_FewerThan_PageSize()
        {           
            var db = GetTestDb("last_page");

            var query = new blog.Data.GetPostsQuery(db);

            var results = await query.ExecuteAsync(page: 4, pageSize: 3);

            Assert.Collection<Post>(results, 
                x => Assert.Equal("Ten", x.Title)
            );
        }
    }
}

using System.Collections.Generic;
using System.Linq;

namespace blog{
    public class Paginator
    {
        public int PageNum { get; }

        public int PageCount { get; }

        public int? PreviousPage {
            get
            {
                if (PageNum == 1)
                {
                    return null;
                }

                return PageNum - 1;
            }
        }

        public int? NextPage {
            get
            {
                if (PageNum < PageCount)
                {
                    return PageNum + 1;
                }

                return null;
            }
         }

         public string PreviousPagePagingSegment
        {
             get
             {
                 //When pointing to the 'page 1', we want to
                 //point to the home page with no paging segment
                 //ie: 'localhost/' instead of 'localhost/page/1'
                 if(PageNum == 2)
                 {
                    return string.Empty;
                 }

                 return "page";
             }
         }

         public string NextPagePagingSegment => "page";

        public IEnumerable<int> PageNumbers { get; }

        public Paginator(int pageNum, int pageCount)
        {
            PageNum = pageNum;

            PageCount = pageCount;
        }

        public int? GetRoutingPageNum(int? pageNum)
        {
            if(pageNum == null)
            {
                return null;
            }

            return GetRoutingPageNum(pageNum.Value);
        }

        public int? GetRoutingPageNum(int pageNum)
        {
            if(pageNum == 1)
            {
                return null;
            }

            return pageNum;
        }

        public IEnumerable<int> GetPagedList()
        {
            if(PageCount <= 10)
            {
                return Enumerable.Range(1, PageCount).ToList();
            }

            List<int> pages;

            //Add first 4 pages
            pages = Enumerable.Range(1, 4).ToList();

            //Add middle page
            pages.Add(PageCount / 2);

            //Add last 4 pages
            pages.AddRange(Enumerable.Range(PageCount - 4, 4));

            return pages;
        }
    }
}

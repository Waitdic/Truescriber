using System;

namespace Truescriber.BLL.PageModel
{
    public class PageViewModel
    {
        public int PageNumber { get; protected set; }
        public int TotalPages { get; protected set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => (PageNumber > 1);

        public bool HasNextPage => (PageNumber < TotalPages);
    }
}

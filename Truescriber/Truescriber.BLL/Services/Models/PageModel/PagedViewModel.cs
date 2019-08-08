using System;

namespace Truescriber.BLL.Services.Models.PageModel
{
    public class PagedViewModel
    {
        public int PageNumber { get; protected set; }
        public int TotalPages { get; protected set; }

        public PagedViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => (PageNumber > 1);

        public bool HasNextPage => (PageNumber < TotalPages);
    }
}

namespace UniversalIdentity.Domain.Filter
{
    public class PaginationFilter
    {
        public const int MaxPagSize = 10;
        public const int DefaultPageNumber = 1;
        private int _pageSize;
        private int _pageNumber;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value < DefaultPageNumber ? DefaultPageNumber : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPagSize ? MaxPagSize : value;
        }

        public PaginationFilter()
        {
            this.PageNumber = DefaultPageNumber;
            this.PageSize = MaxPagSize;
        }

        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}

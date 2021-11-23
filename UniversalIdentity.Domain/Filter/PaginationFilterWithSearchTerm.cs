namespace UniversalIdentity.Domain.Filter
{
    public class PaginationFilterWithSearchTerm : PaginationFilter
    {
        public string SearchTerm { get; set; }

        public PaginationFilterWithSearchTerm() : base()
        {

        }

        public PaginationFilterWithSearchTerm(int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {

        }
    }
}

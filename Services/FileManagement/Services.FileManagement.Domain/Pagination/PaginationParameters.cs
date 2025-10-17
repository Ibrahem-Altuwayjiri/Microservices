

namespace Services.FileManagement.Domain.Pagination
{
    public class PaginationParameters
    {
        // current page number
        public int PageIndex { get; set; } = 0;
        // total of entities to show in single page
        public int PageSize { get; set; } = 10;
    }
}

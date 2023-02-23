using Microsoft.EntityFrameworkCore;

namespace RecorridoHistoricoApi.Utils.Pagination
{
    public class PaginatedList<T>
    {
        public int CurrentPage { get; private set; }
        public int RecordsCount { get; private set; }
        public int PagesCount { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public List<T> Records { get; private set; }

        private PaginatedList(List<T> records, int recordsCount, int currentPage, int recordsPerPage)
        {
            Records = records;
            RecordsCount = recordsCount;
            CurrentPage = currentPage;
            PagesCount = (int)Math.Ceiling(recordsCount / (double)recordsPerPage);
            HasPreviousPage = CurrentPage > 1;
            HasNextPage = CurrentPage < PagesCount;
            Records = records;
        }

        public static async Task<PaginatedList<T>> PaginateAsync(IQueryable<T> source, PaginationParams paginationParams)
        {
            const int defaultPageSize = 15, defaultCurrentPage = 1;
            int recordsPerPage =  paginationParams.RecordsPerPage ?? defaultPageSize;
            int currentPage = paginationParams.PageNumber ?? defaultCurrentPage;

            var recordsCount = await source.CountAsync();
            var records = await source.Skip((currentPage - 1) * recordsPerPage).Take(recordsPerPage).ToListAsync();
            return new PaginatedList<T>(records, recordsCount, currentPage, recordsPerPage);
        }

    }
}

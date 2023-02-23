using Microsoft.EntityFrameworkCore;

namespace EdecanesV2.Utils.Pagination
{
    /// <summary>
    /// El objetivo de esta clase es para poder mapear los resultados de la paginacion a los DTOs correspondientes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedResults<T>
    {
        public int CurrentPage { get; private set; }
        public int RecordsCount { get; private set; }
        public int PagesCount { get; private set; }
        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public IEnumerable<T> Records { get; set; } = new List<T>();
    }
}

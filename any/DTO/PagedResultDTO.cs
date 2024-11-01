namespace any.DTO
{
    public class PagedResultDTO<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Items { get; set; }

        public PagedResultDTO(int total, IEnumerable<T> items)
        {
            Total = total;
            Items = items;
        }
    }
}

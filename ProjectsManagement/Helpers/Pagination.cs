namespace ProjectManagementSystem.Helper
{
    public class Pagination<T>
    {
        public Pagination(int pageSize, int pageIndex, int count, IEnumerable<T> data)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            Count = count;

        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}

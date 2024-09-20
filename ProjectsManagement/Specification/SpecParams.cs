
namespace ProjectManagementSystem.Repository.Specification
{
    public class SpecParams
    {
        public string? Sort { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
        public string? Search { get; set; }

    }
}
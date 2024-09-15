namespace ProjectsManagement.Data
{
    public class ApiValidationErrorssResponse : ApiResponse
    {

        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorssResponse() : base(400)
        {

            Errors = new List<string>();
        }
    }
}

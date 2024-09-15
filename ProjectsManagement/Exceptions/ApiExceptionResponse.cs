namespace ProjectsManagement.Data
{
    public class ApiExceptionResponse : ApiResponse
    {
        public ApiExceptionResponse(int statuscode, string? message = null , string? details = null) : base(statuscode, message)
        {

            Details = details;
        }

        public string? Details { get; set; }

    }
}

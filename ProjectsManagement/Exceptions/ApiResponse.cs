namespace ProjectsManagement.Data
{
    public class ApiResponse
    {

        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public ApiResponse(int statuscode , string? message = null)
        {

            StatusCode = statuscode;
            Message = message ?? GetDefaultMessageForStatusCode(statuscode);

        }

        private string? GetDefaultMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "Bad Request",
                401 => "UnAuthrized",
                404 => "Not Found",
                500 => "Server Error",
                 _ => null
            } ;
        }

        
    }
   
    
}

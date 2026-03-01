namespace Store.Api.Errors
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ApiErrorResponse(int statusCode, string? message=null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultResponse(StatusCode);
        }

        private string? GetDefaultResponse(int statusCode)
        {
            var message = StatusCode switch
            {
                400 => "A Bad Request,You Have Made",
                401 => "Authorized Error , You Are Not Allow",
                404 => "Resources Not Found",
                500 => "Server Error",
                _=> null
            };
            return message;
        }
    }
}

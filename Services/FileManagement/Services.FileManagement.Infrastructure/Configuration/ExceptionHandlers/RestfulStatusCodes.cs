namespace Services.FileManagement.Infrastructure.Configuration.ExceptionHandlers
{
    public static class RestfulStatusCodes
    {
        public const int NotFound = 404; // When database has no value requested
        public const int Unauthorized = 401; // User not login to make an action or wrong credentials
        public const int BadRequest = 400; // User sent invalid request ( missing parameter etc.. )
        public const int Forbidden = 403; // User logged-in but don't have premission to do an action which requires higher privilege
        public const int Conflict = 409; // Wordflow is not in order 
        public const int PreconditionFailed = 412; // Missing required HTTP header
        public const int InternalServerError = 500; // 3rd party API not responding
        public const int ServiceUnavailable = 503; // 3rd party API not responding
    }
}

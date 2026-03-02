namespace Services.Frontend.Web.Enum
{
    public class SD
    {
        // ✅ CENTRALIZED GATEWAY BASE URL
        //public const string GatewayBaseUrl = "https://localhost:7777";
        public const string GatewayBaseUrl = "http://localhost:5295";

        public const string TokenCookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public enum ContentType
        {
            Json,
            MultipartFormData,
        }

    }
}

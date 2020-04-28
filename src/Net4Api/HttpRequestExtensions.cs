using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace NetApi
{
    public static class HttpRequestExtensions
    {
        public static string GetBearerAuthorizationToken(this HttpRequestMessage request)
        {
            if (request == null)
                return null;

            IEnumerable<string> headerValues;
            if (request.Headers != null &&
                request.Headers.TryGetValues("Authorization", out headerValues))
            {
                var value = headerValues.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(value) && value.Contains("Bearer"))
                    return value.Replace("Bearer ", "").Trim();
            }

            return null;
        }
    }
}

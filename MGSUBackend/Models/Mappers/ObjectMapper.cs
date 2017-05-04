using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace MGSUBackend.Models.Mappers
{
    public static class ObjectMapper
    {
        public static HttpResponseMessage ObjectToHttpResponseMessage<T>(T @object)
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<T>(@object, new JsonMediaTypeFormatter(), "application/json")
            };
        }
    }
}
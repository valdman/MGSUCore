using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MGSUCore.Authentification
{
    public class AuthenticationFilter : IAsyncAuthorizationFilter
    {
        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
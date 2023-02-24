using MimunYashirInfrastructure.Cummon;
using MimunYashirInfrastructure.Identity;

namespace MimunYashir
{
    public class WebAppContext : IAppContext
    {
        public WebAppContext(IHttpContextAccessor httpContextAccessor)
        {

            setMembers(httpContextAccessor.HttpContext);

        }
        private void setMembers(HttpContext httpContext)
        {
            if (httpContext != null && httpContext.User != null && httpContext.User.Identity != null)
            {

                UserId = getClaim(httpContext, IdentityClaims.USER_CLAIM_ID);


            }
            else
            {
                UserId = "Anonymous";
            }
        }
        private string getClaim(HttpContext httpContext, string claimKey)
        {
            var claim = httpContext.User.Claims.FirstOrDefault(x => x.Type == claimKey);
            return claim?.Value;
        }
        public string UserId { get; private set; }
    }
}

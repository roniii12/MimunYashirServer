using Microsoft.AspNetCore.Authorization;

namespace MimunYashir.Controllers
{
    [Authorize]
    public abstract class AuthBaseController : _baseController
    {
        public AuthBaseController(WebAppContext context) : base(context)
        {
        }
    }
}

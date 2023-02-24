using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MimunYashirInfrastructure.Exceptions;

namespace MimunYashir.Controllers
{
    public abstract class _baseController : ControllerBase
    {
        protected readonly WebAppContext _context;

        public _baseController(WebAppContext context)
        {
            _context = context;
        }

        protected ActionResult<T> ReturnException<T>(Exception ex)
        {
            if (ex is ReturnToClientException)
            {
                return BadRequest(ex.Message);
            }
            else if (ex is ManagedException)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return BadRequest("");
        }
        protected ActionResult ReturnException(Exception ex)
        {
            if (ex is ReturnToClientException)
            {
                return BadRequest(ex.Message);
            }
            else if (ex is ManagedException)
            {
                return BadRequest(ex.InnerException.Message);
            }
            return BadRequest("");
        }

        protected string getRequestHeader(string requestKey)
        {
            StringValues parameter;
            if (HttpContext.Request.Headers.TryGetValue(requestKey, out parameter))
                return parameter.ToString();
            return null;
        }
    }
}

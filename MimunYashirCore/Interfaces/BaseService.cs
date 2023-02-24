using MimunYashirInfrastructure.Cummon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Interfaces
{
    public abstract class BaseService
    {
        protected IAppContext _context;
        protected BaseService(IAppContext _context)
        {
            this._context = _context;
        }


        protected void validateUserContext()
        {
            if (string.IsNullOrEmpty(_context.UserId))
                throw new ArgumentException($"UserId is not defined in context");

            //if (!_context.UserAskiRowId.HasValue)
            //    throw new ArgumentException($"UserAskiRowId is not defined in context");
        }

    }
}

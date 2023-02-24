using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirInfrastructure.Exceptions
{
    public class ReturnToClientException : Exception
    {
        public ReturnToClientException(string message)
            : base(message)
        {

        }
    }
}

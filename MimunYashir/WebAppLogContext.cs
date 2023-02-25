using MimunYashirInfrastructure.Cummon;
using MimunYashirInfrastructure.Log;

namespace MimunYashir
{
    public class WebAppLogContext : IAppLogContext
    {

        public string UserId { get; private set; }

        public WebAppLogContext(IAppContext serviceContext)
        {

            UserId = serviceContext.UserId;
        }
    }
}

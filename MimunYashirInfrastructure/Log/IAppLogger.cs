using MimunYashirInfrastructure.Cummon;
using MimunYashirInfrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirInfrastructure.Log
{
    public interface IAppLogger<T>
    {
        void Error(ManagedException exception);
        void Error(string message);
        void Error(string message, Exception exception);
        void Info(string message);
        void Info(string message, object parameters, AppModule module, AppLayer layer);
        void Warning(string message);
        void Warning(string message, object parameters);
        void Warning(ManagedException exception);
    }

    public interface IAppLogContext : IAppContext
    {

    }
}

using MimunYashirInfrastructure.Cummon;
using MimunYashirPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Interfaces
{
    public interface IAccountService
    {
        public Task<string> GetCustomerIdByIdAsync(string Id);
    }
}

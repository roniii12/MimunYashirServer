using MimunYashirCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Interfaces
{
    public interface ICustomerService
    {
        public Task<CustomerModel> GetCustomerDetailsAsync();
        public Task<CustomerModel> UpdateCustomerAddressAsync(UpdateAddressModel addressModel);
    }
}

using MimunYashirCore.Interfaces;
using MimunYashirInfrastructure.Cummon;
using MimunYashirPersistence;
using MimunYashirPersistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Services
{
    public class AccountService : BaseService, IAccountService
    {
        private readonly IAsyncRepository<Customer> _customerRepo;
        public AccountService(IAppContext context,
            IAsyncRepository<Customer> customerRepo
            ) : base(context)
        {
            _customerRepo = customerRepo;
        }

        public async Task<Customer> FindCustomerByIdAsync(string id)
        {
            var query = new QueryBaseSpecification<Customer>(x => x.Id == id);
            var customer = await _customerRepo.FirstOrDefaultAsync(query).ConfigureAwait(false);
            return customer;
        }
    }
}

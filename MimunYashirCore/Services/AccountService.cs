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
        private readonly IMemoryCacheService _cacheService;
        public AccountService(IAppContext context,
            IAsyncRepository<Customer> customerRepo,
            IMemoryCacheService cacheService
            ) : base(context)
        {
            _customerRepo = customerRepo;
            _cacheService = cacheService;
        }

        public async Task<string> GetCustomerIdByIdAsync(string id)
        {
            string key = IMemoryCacheService.CUSTOMER_ID + id;
            var customerId = (string)_cacheService.TryGetObjectFromCache(key);
            if (customerId != null) return customerId;
            var query = new QueryBaseSpecification<Customer>(x => x.Id == id);
            var customer = await _customerRepo.FirstOrDefaultAsync(query).ConfigureAwait(false);
            customerId = customer?.Id;
            _cacheService.UpdateOrCreateCache(key, customerId, 10, 60);
            return customerId;
        }
    }
}

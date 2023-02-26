using MimunYashirCore.Interfaces;
using MimunYashirInfrastructure.Cummon;
using MimunYashirPersistence.Repositories;
using MimunYashirPersistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MimunYashirCore.Models;
using AutoMapper;

namespace MimunYashirCore.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IAsyncRepository<Customer> _customerRepo;
        private readonly IMapper _mapper;
        private readonly IMemoryCacheService _cacheService;
        public CustomerService(IAppContext context,
            IAsyncRepository<Customer> customerRepo,
            IMemoryCacheService cacheService,
            IMapper mapper
            ) : base(context)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<CustomerModel> GetCustomerDetailsAsync()
        {
            string key = IMemoryCacheService.CUSTOMER_DETAILS + _context.UserId;
            CustomerModel customerModel = (CustomerModel)_cacheService.TryGetObjectFromCache(key);
            if (customerModel != null) return customerModel;

            var query = new QueryBaseSpecification<Customer>(cust => cust.Id == _context.UserId);
            query.AddInclude("Contracts.Packages");
            var customer = await _customerRepo.FirstOrDefaultAsync(query).ConfigureAwait(false);
            customerModel = _mapper.Map<CustomerModel>(customer);
            _cacheService.UpdateOrCreateCache(key, customerModel);

            return customerModel;
        }

        public async Task<CustomerModel> UpdateCustomerAddressAsync(UpdateAddressModel addressModel)
        {
            var query = new QueryBaseSpecification<Customer>(cust => cust.Id == _context.UserId);
            var customer = await _customerRepo.FirstOrDefaultAsync(query).ConfigureAwait(false);
            customer = _mapper.Map<UpdateAddressModel, Customer>(addressModel, customer);
            await _customerRepo.UpdateAsync(customer).ConfigureAwait(false);
            var customerModel = _mapper.Map<CustomerModel>(customer);
            _cacheService.DeleteCache(IMemoryCacheService.CUSTOMER_DETAILS + _context.UserId);
            return customerModel;
        }
    }
}

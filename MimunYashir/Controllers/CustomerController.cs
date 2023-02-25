using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimunYashir.Controllers.Account;
using MimunYashirCore.Interfaces;
using MimunYashirCore.Models;
using MimunYashirInfrastructure.Cummon;
using MimunYashirInfrastructure.Exceptions;
using MimunYashirInfrastructure.Log;

namespace MimunYashir.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : AuthBaseController
    {
        private readonly ICustomerService _customerService;
        private readonly IAppLogger<AuthenticateController> _logger;

        public CustomerController(WebAppContext context,
            IAppLogger<AuthenticateController> logger,
            ICustomerService customerService) : base(context)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpGet("CustomerDetails")]
        public async Task<ActionResult<CustomerModel>> GetCustomerDetails()
        {
            try
            {
                var customer = await _customerService.GetCustomerDetailsAsync().ConfigureAwait(false);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.Error(new ManagedException(ex, "Failed to GetCustomerDetails", AppModule.CUSTOMER, AppLayer.WEB_API));
                return ReturnException<CustomerModel>(ex);
            }
        }
        [HttpPost("UpdateCustomerAddress")]
        public async Task<ActionResult<CustomerModel>> UpdateCutomerAddress([FromBody] UpdateAddressModel updateAddress)
        {
            try
            {
                var customer = await _customerService.UpdateCustomerAddressAsync(updateAddress).ConfigureAwait(false);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.Error(new ManagedException(ex, "Failed to UpdateCutomerAddress", AppModule.CUSTOMER, AppLayer.WEB_API));
                return ReturnException<CustomerModel>(ex);
            }
        }
    }
}

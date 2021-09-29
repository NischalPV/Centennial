using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Centennial.Api.Infrastructure.Services;
using Centennial.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomersController> _logger;
        private readonly IIdentityService _identityService;

        public CustomersController(ICustomerRepository customerRepository, ILogger<CustomersController> logger, IIdentityService identityService)
        {
            _customerRepository = customerRepository;
            _logger = logger;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            return Ok(await _customerRepository.ListAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomer(string Id)
        {
            return Ok(await _customerRepository.GetByIdAsync(id: Id));
        }

        [HttpPost]
        public async Task<IActionResult> PostCustomer(Entities.Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.CreatedBy = _identityService.GetUserIdentity();
                try
                {
                    await _customerRepository.AddAsync(customer);
                    return CreatedAtAction(nameof(GetCustomer), new { Id = customer.Id }, customer);
                }
                catch(Exception ex)
                {
                    _logger.LogError($"Error while creating Customer: {ex.Message}");
                    throw;
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutCustomer(string Id, Entities.Customer customer)
        {
            var dbCustomer = await _customerRepository.GetByIdAsync(Id);

            if (dbCustomer == null)
            {
                return BadRequest();
            }

            try
            {

                dbCustomer.Name = customer.Name;
                dbCustomer.PhoneNumber = customer.PhoneNumber;
                dbCustomer.Address = customer.Address;

                await _customerRepository.UpdateAsync(dbCustomer);

                return Ok(dbCustomer);
            }
            catch(Exception ex)
            {
                if(! await CustomerExists(customer))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error while updating Customer: {ex.Message}");
                    throw;
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCustomer(string Id)
        {
            var customer = await _customerRepository.GetByIdAsync(Id);

            if(customer != null)
            {
                await _customerRepository.DeleteAsync(customer);
                return NoContent();
            }

            return NotFound();
        }

        private async Task<bool> CustomerExists(Entities.Customer customer)
        {
            return await _customerRepository.IsExists(customer, x => x.IsActive);
        }
    }
}
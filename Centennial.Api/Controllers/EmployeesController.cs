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
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeesController> _logger;
        private readonly IIdentityService _identityService;

        public EmployeesController(IEmployeeRepository employeeRepository, ILogger<EmployeesController> logger, IIdentityService identityService)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            // var loggedInUser = _identityService.GetUserName();
            return Ok(await _employeeRepository.ListAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetEmployee(string Id)
        {
            return Ok(await _employeeRepository.GetByIdAsync(id: Id));
        }

        [HttpPost]
        public async Task<IActionResult> PostEmployee(Entities.Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.CreatedBy = _identityService.GetUserIdentity();
                try
                {
                    await _employeeRepository.AddAsync(employee);
                    return CreatedAtAction(nameof(GetEmployee), new { Id = employee.Id }, employee);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error while creating Employee: {ex.Message}");
                    throw;
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutEmployee(string Id, Entities.Employee employee)
        {
            var dbEmployee = await _employeeRepository.GetByIdAsync(Id);

            if (dbEmployee == null)
            {
                return BadRequest();
            }

            try
            {

                dbEmployee.Name = employee.Name;

                await _employeeRepository.UpdateAsync(dbEmployee);

                return Ok(dbEmployee);
            }
            catch (Exception ex)
            {
                if (!await EmployeeExists(employee))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error while updating Employee details: {ex.Message}");
                    throw;
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEmployee(string Id)
        {
            var material = await _employeeRepository.GetByIdAsync(Id);

            if (material != null)
            {
                await _employeeRepository.DeleteAsync(material);
                return NoContent();
            }

            return NotFound();
        }

        private async Task<bool> EmployeeExists(Entities.Employee employee)
        {
            return await _employeeRepository.IsExists(employee, x => x.IsActive);
        }
    }
}
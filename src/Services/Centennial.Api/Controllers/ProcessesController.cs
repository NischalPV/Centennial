using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Centennial.Api.Data;
using Centennial.Api.Entities;
using Microsoft.AspNetCore.Authorization;
using Centennial.Api.Interfaces;
using Centennial.Api.Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace Centennial.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ProcessesController : ControllerBase
    {
        private readonly IAsyncRepository<Process, int> _processRepository;
        private readonly ILogger<ProcessesController> _logger;
        private readonly IIdentityService _identityService;

        public ProcessesController(IAsyncRepository<Process, int> processRepository, ILogger<ProcessesController> logger, IIdentityService identityService)
        {
            _processRepository = processRepository ?? throw new ArgumentNullException(nameof(processRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        public async Task<IActionResult> GetProcesses()
        {

            return Ok(await _processRepository.ListAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProcess(int Id)
        {
            return Ok(await _processRepository.GetByIdAsync(id: Id));
        }

        [HttpPost]
        public async Task<IActionResult> PostProcess(Process process)
        {
            if (ModelState.IsValid)
            {
                process.CreatedBy = _identityService.GetUserIdentity();
                try
                {
                    await _processRepository.AddAsync(process);
                    return CreatedAtAction(nameof(GetProcess), new { Id = process.Id }, process);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error while creating Product: {ex.Message}");
                    throw;
                }
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProcess(int Id)
        {
            var process = await _processRepository.GetByIdAsync(Id);

            if (process != null)
            {
                if (process.IsRemovable && !process.IsMandatory)
                {
                    await _processRepository.DeleteAsync(process);
                    return NoContent();
                }
                else
                {
                    return BadRequest($"Process {process.Name} cannot be deleted.");
                }

            }

            return NotFound();
        }

        private async Task<bool> ProcessExists(Process process)
        {
            return await _processRepository.IsExists(process, x => x.IsActive);
        }
    }
}

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
    public class RawMaterialsController : ControllerBase
    {
        private readonly IRawMaterialRepository _rawMaterialRepository;
        private readonly ILogger<RawMaterialsController> _logger;
        private readonly IIdentityService _identityService;

        public RawMaterialsController(IRawMaterialRepository rawMaterialRepository, ILogger<RawMaterialsController> logger, IIdentityService identityService)
        {
            _rawMaterialRepository = rawMaterialRepository ?? throw new ArgumentNullException(nameof(rawMaterialRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        public async Task<IActionResult> GetRawMaterials()
        {
            // var loggedInUser = _identityService.GetUserName();
            return Ok(await _rawMaterialRepository.ListAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRawMaterial(string Id)
        {
            return Ok(await _rawMaterialRepository.GetByIdAsync(id: Id));
        }

        [HttpPost]
        public async Task<IActionResult> PostRawMaterial(Entities.RawMaterial rawMaterial)
        {
            if (ModelState.IsValid)
            {
                rawMaterial.CreatedBy = _identityService.GetUserIdentity();
                try
                {
                    await _rawMaterialRepository.AddAsync(rawMaterial, doSave: true);
                    return CreatedAtAction(nameof(GetRawMaterial), new { Id = rawMaterial.Id }, rawMaterial);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error while creating Raw material: {ex.Message}");
                    throw;
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutRawMaterial(string Id, Entities.RawMaterial rawMaterial)
        {
            var dbRawMaterial = await _rawMaterialRepository.GetByIdAsync(Id);

            if (dbRawMaterial == null)
            {
                return BadRequest();
            }

            try
            {

                dbRawMaterial.Name = rawMaterial.Name;

                await _rawMaterialRepository.UpdateAsync(dbRawMaterial);

                return Ok(dbRawMaterial);
            }
            catch (Exception ex)
            {
                if (!await RawMaterialExists(rawMaterial))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error while updating Raw Material details: {ex.Message}");
                    throw;
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteRawMaterial(string Id)
        {
            var material = await _rawMaterialRepository.GetByIdAsync(Id);

            if (material != null)
            {
                await _rawMaterialRepository.DeleteAsync(material);
                return NoContent();
            }

            return NotFound();
        }

        private async Task<bool> RawMaterialExists(Entities.RawMaterial rawMaterial)
        {
            return await _rawMaterialRepository.IsExists(rawMaterial, x => x.IsActive);
        }
    }
}
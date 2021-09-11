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
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly ILogger<MaterialsController> _logger;
        private readonly IIdentityService _identityService;

        public MaterialsController(IMaterialRepository materialRepository, ILogger<MaterialsController> logger, IIdentityService identityService)
        {
            _materialRepository = materialRepository ?? throw new ArgumentNullException(nameof(materialRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        [HttpGet]
        public async Task<IActionResult> GetMaterials()
        {
            // var loggedInUser = _identityService.GetUserName();
            return Ok(await _materialRepository.ListAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetMaterial(string Id)
        {
            return Ok(await _materialRepository.GetByIdAsync(id: Id));
        }

        [HttpPost]
        public async Task<IActionResult> PostMaterial(Entities.Material material)
        {
            if (ModelState.IsValid)
            {
                material.CreatedBy = _identityService.GetUserIdentity();
                try
                {
                    await _materialRepository.AddAsync(material);
                    return CreatedAtAction(nameof(GetMaterial), new { Id = material.Id }, material);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error while creating Material: {ex.Message}");
                    throw;
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutMaterial(string Id, Entities.Material material)
        {
            var dbMaterial = await _materialRepository.GetByIdAsync(Id);

            if (dbMaterial == null)
            {
                return BadRequest();
            }

            try
            {

                dbMaterial.Name = material.Name;

                await _materialRepository.UpdateAsync(dbMaterial);

                return Ok(dbMaterial);
            }
            catch (Exception ex)
            {
                if (!await MaterialExists(material))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error while updating Material: {ex.Message}");
                    throw;
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteMaterial(string Id)
        {
            var material = await _materialRepository.GetByIdAsync(Id);

            if (material != null)
            {
                await _materialRepository.DeleteAsync(material);
                return NoContent();
            }

            return NotFound();
        }

        private async Task<bool> MaterialExists(Entities.Material material)
        {
            return await _materialRepository.IsExists(material, x => x.IsActive);
        }
    }
}
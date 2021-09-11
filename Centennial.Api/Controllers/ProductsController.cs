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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductsController> _logger;
        private readonly IIdentityService _identityService;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger, IIdentityService identityService)
        {
            _productRepository = productRepository;
            _logger = logger;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productRepository.ListAllAsync());
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProduct(string Id)
        {
            return Ok(await _productRepository.GetByIdAsync(id: Id));
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(Entities.Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedBy = _identityService.GetUserIdentity();
                try
                {
                    await _productRepository.AddAsync(product);
                    return CreatedAtAction(nameof(GetProduct), new { Id = product.Id }, product);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error while creating Product: {ex.Message}");
                    throw;
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> PutProduct(string Id, Entities.Product product)
        {
            var dbProduct = await _productRepository.GetByIdAsync(Id);

            if (dbProduct == null)
            {
                return BadRequest();
            }

            try
            {

                dbProduct.Name = product.Name;
                dbProduct.Dimensions = product.Dimensions;
                dbProduct.Price = product.Price;
                dbProduct.UpdatedDate = DateTime.UtcNow;
                dbProduct.UpdatedBy = _identityService.GetUserIdentity();

                await _productRepository.UpdateAsync(dbProduct);

                return Ok(dbProduct);
            }
            catch (Exception ex)
            {
                if (!await ProductExists(product))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"Error while updating Product: {ex.Message}");
                    throw;
                }
            }

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteProduct(string Id)
        {
            var product = await _productRepository.GetByIdAsync(Id);

            if (product != null)
            {
                await _productRepository.DeleteAsync(product);
                return NoContent();
            }

            return NotFound();
        }

        private async Task<bool> ProductExists(Entities.Product product)
        {
            return await _productRepository.IsExists(product, x => x.IsActive);
        }
    }
}

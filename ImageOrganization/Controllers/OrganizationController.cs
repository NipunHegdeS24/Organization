using ImageOrganization.Application.Dto;
using ImageOrganization.Application.Services;
using ImageOrganization.Domian.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ImageOrganization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationService _organizationService;

        public OrganizationController(OrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrganizations()
        {
            var organization = await _organizationService.GetOrganizations();
            return Ok(organization);
        }

        [HttpGet("{organizationId}")]

        public async Task<IActionResult> GetOrganizationById(Guid organizationId)
        {
            var organzation = await _organizationService.GetOrganizationById(organizationId);

            if (organzation == null) 
                return NotFound();

            return Ok(organzation);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization ([FromForm] OrganizationDto organization, IFormFile logo)
        {
            var createdOrganization = await _organizationService.CreateOrganization(organization, logo);

            return CreatedAtAction(nameof(GetOrganizationById), new { organizationId = createdOrganization.OrganizationId }, createdOrganization);
        }

        [HttpPut("{organizationId}")]
        public async Task<IActionResult> UpdateOrganization(Guid OrganizationId,[FromForm] OrganizationDto updateOrganization, IFormFile logo)
        {
            var updatedOrganization = await _organizationService.UpdateOrganization(OrganizationId, updateOrganization, logo);

            if (updatedOrganization == null)
                return NotFound();

            return Ok(updatedOrganization);
        }

        [HttpDelete("{organizationId}")]
        public async Task<IActionResult> DeleteOrganization(Guid organizationId)
        {
            var organization = await _organizationService.DeleteOrganization(organizationId);

            if(organization == false)
            {
                return NotFound();
            }

            return Ok("Organization has been deleted");

        }
    }
}

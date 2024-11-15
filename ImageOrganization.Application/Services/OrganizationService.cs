using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageOrganization.Application.Dto;
using ImageOrganization.Application.Interface;
using ImageOrganization.Domian.Models;
using Microsoft.AspNetCore.Http;


namespace ImageOrganization.Application.Services
{
    public class OrganizationService 
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly string _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");
        public OrganizationService(IOrganizationRepository organizationRepository) 
        {
            _organizationRepository = organizationRepository;

            //Ensure the upload directory exists
            if(!Directory.Exists(_uploadDirectory))
            {
                Directory.CreateDirectory(_uploadDirectory);
            }
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await _organizationRepository.GetOrganizations();
        }

        public async Task<Organization> GetOrganizationById(Guid OrganizationId)
        {
            return await _organizationRepository.GetOrganizationById(OrganizationId);
        }

        public async Task<Organization> CreateOrganization(OrganizationDto organization, IFormFile logo)
        {
            Organization org = new Organization
            {
                Name = organization.Name,
                Email = organization.Email,
                Contact = organization.Contact,
                City = organization.City,
                Country = organization.Country,
                StreetAddress = organization.StreetAddress,
                PostalCode = organization.PostalCode,
            };

            if (logo != null && logo.Length > 0)
            {
                var filePath = Path.Combine(_uploadDirectory, logo.FileName);
                //Save logo in the file

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logo.CopyToAsync(stream); 
                }

                org.Logo = filePath;
               
            }
            return await _organizationRepository.CreateOrganization(org);
        }

        public async Task<Organization> UpdateOrganization(Guid OrganizationId, OrganizationDto updateOrganization, IFormFile logo)
        {
            var organization = await _organizationRepository.GetOrganizationById(OrganizationId);



            if (organization == null)
                return null;

            organization.Name = updateOrganization.Name;
            organization.Email = updateOrganization.Email;
            organization.Contact = updateOrganization.Contact;
            organization.Country = updateOrganization.Country;
            organization.City = updateOrganization.City;
            organization.StreetAddress = updateOrganization.StreetAddress;
            organization.PostalCode = updateOrganization.PostalCode;

            if(logo != null && logo.Length>0)
            {
                //Delete the existing logo file if it exists
                if(!string.IsNullOrEmpty(organization.Logo) && File.Exists(organization.Logo))
                {
                    File.Delete(organization.Logo);
                }

                //Save the new Logo
                var filePath = Path.Combine(_uploadDirectory, logo.FileName);

                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    await logo.CopyToAsync(stream);
                }
                organization.Logo = filePath;
            }
            return await _organizationRepository.UpdateOrganization(OrganizationId, updateOrganization);
        }

        public async Task<bool> DeleteOrganization(Guid OrganizationId)
        {
            var organization = await _organizationRepository.GetOrganizationById(OrganizationId);

            if (organization == null)
                return false;

            if (!string.IsNullOrEmpty(organization.Logo) && File.Exists(organization.Logo))
            {
                File.Delete(organization.Logo);
            }
            return await _organizationRepository.DeleteOrganization(OrganizationId);
        }
    }
}

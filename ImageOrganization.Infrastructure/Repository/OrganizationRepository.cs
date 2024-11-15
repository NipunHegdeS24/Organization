using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageOrganization.Application.Dto;
using ImageOrganization.Application.Interface;
using ImageOrganization.Domian.Models;
using ImageOrganization.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ImageOrganization.Infrastructure.Repository
{
    public class OrganizationRepository : IOrganizationRepository 
    {
        private readonly AppDbContext _appDbContext;

        public OrganizationRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Organization>> GetOrganizations()
        {
            return await _appDbContext.organizations.ToListAsync();
        }

        public async Task<Organization> GetOrganizationById(Guid OrganizationId)
        {
            var res = await _appDbContext.organizations.FindAsync(OrganizationId);
            Console.WriteLine(res);
            return await _appDbContext.organizations.FindAsync(OrganizationId);
        }

        public async Task<Organization> CreateOrganization(Organization organization)
        {
            organization.OrganizationId = Guid.NewGuid();
            _appDbContext.organizations.Add(organization);
            await _appDbContext.SaveChangesAsync();
            return organization;
        }

        public async Task<Organization> UpdateOrganization(Guid OrganizationId, OrganizationDto updateOrganization)
        {
            var organization = await _appDbContext.organizations.FirstOrDefaultAsync(x => x.OrganizationId == OrganizationId);


            if (organization != null)
            {
                organization.Name = updateOrganization.Name;
                organization.Email = updateOrganization.Email;
                organization.Contact = updateOrganization.Contact;
                organization.Country = updateOrganization.Country;
                organization.City = updateOrganization.City;
                organization.StreetAddress = updateOrganization.StreetAddress;
                organization.PostalCode = updateOrganization.PostalCode;

                await _appDbContext.SaveChangesAsync(); 
                return organization;
            }    
            return new Organization
            {
                Name = updateOrganization.Name,
                Email = updateOrganization.Email,
                Contact = updateOrganization.Contact,
                Country = updateOrganization.Country,
                City = updateOrganization.City,
                StreetAddress = updateOrganization.StreetAddress,
                PostalCode = updateOrganization.PostalCode,
            };
        }

        public async Task<bool> DeleteOrganization(Guid OrganizationId)
        {
            var organization = await _appDbContext.organizations.FirstOrDefaultAsync(x => x.OrganizationId == OrganizationId);

            if (organization != null)
            {
                _appDbContext.organizations.Remove(organization);
                await _appDbContext.SaveChangesAsync();
            }
            return false;
        }
    }
}

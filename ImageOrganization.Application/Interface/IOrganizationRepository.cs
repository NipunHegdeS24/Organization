using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageOrganization.Application.Dto;
using ImageOrganization.Domian.Models;

namespace ImageOrganization.Application.Interface
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetOrganizations();
        Task<Organization> GetOrganizationById(Guid OrganizationId);
        Task<Organization> CreateOrganization(Organization organization);
        Task<Organization> UpdateOrganization(Guid OrganizationId, OrganizationDto organization);
        Task<bool> DeleteOrganization (Guid OrganizationId);
    }
}

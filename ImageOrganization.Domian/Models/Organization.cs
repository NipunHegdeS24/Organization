using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOrganization.Domian.Models
{
    public class Organization
    {
        public Guid OrganizationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Logo { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Country { get; set; }
        public int City { get; set; }
        public string StreetAddress { get; set; } = string.Empty;
        public int PostalCode { get; set; }
    }

}

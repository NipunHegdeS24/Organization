using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageOrganization.Application.Dto
{
    public class OrganizationDto
    {
        public string Name { get; set; }
       // public string Logo { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int Country { get; set; }
        public int City { get; set; }
        public string StreetAddress { get; set; }
        public int PostalCode { get; set; }
    }
}

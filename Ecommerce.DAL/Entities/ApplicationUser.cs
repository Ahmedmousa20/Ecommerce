using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsAgree { get; set; }
        public string DisplayName { get; set; }

    }
}

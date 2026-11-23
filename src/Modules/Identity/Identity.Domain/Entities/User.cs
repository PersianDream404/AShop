using Microsoft.AspNetCore.Identity;
using SharedKernel.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Domain.Entities;

public class ApplicationUser : IdentityUser<long>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<Address> Addresses { get; private set; } = [];
}

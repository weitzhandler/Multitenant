﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework.Tenant;
using Microsoft.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace App.Models
{
  public class ApplicationDbContext : TenantIdentityDbContext<ApplicationUser, ApplicationRole, ApplicationTenant>
  {
    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      // Customize the ASP.NET Identity model and override the defaults if needed.
      // For example, you can rename the ASP.NET Identity table names and more.
      // Add your customizations after calling base.OnModelCreating(builder);
    }
  }
}

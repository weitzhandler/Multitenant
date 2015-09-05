using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Identity.EntityFramework.Tenant
{
  public class TenantUser : TenantUser<string>
  {
    public TenantUser()
    {
      Id = Guid.NewGuid().ToString();
    }
  }

  public class TenantUser<TKey> : IdentityUser<TKey>
    where TKey : IEquatable<TKey>
  {
    public virtual TKey TenantId { get; set; }

    public virtual Tenant<TKey> Tenant { get; set; }
  }

  public class TenantUserValidator : TenantUserValidator<string>
  {
  }

  public class TenantUserValidator<TKey> : UserValidator<TenantUser<TKey>>
    where TKey : IEquatable<TKey>
  {
    public override Task<IdentityResult> ValidateAsync(UserManager<TenantUser<TKey>> manager, TenantUser<TKey> user)
    {
      return base.ValidateAsync(manager, user);
    }
  }
}
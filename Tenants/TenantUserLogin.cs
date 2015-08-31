using System;
using System.ComponentModel.DataAnnotations;

namespace Microsoft.AspNet.Identity.EntityFramework.Tenant
{
  public class TenantUserLogin : TenantUserLogin<string>
  {
  }

  public class TenantUserLogin<TKey> : IdentityUserLogin<TKey>
    where TKey : IEquatable<TKey>
  {
    public TKey TenantId { get; set; }
  }
}
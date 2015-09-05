using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Microsoft.AspNet.Identity.EntityFramework.Tenant
{
  public class Tenant : Tenant<string>
  {
  }

  /// <summary>
  /// A class that encapsulates an organization.
  /// </summary>  
  public class Tenant<TKey> : IValidatableObject
    where TKey : IEquatable<TKey>
  {
    //TODO: should retrieve from config.json AppSettings.ReservedSubdomains
    public static string[] ReservedTenantNames = new string[] { "www", "global", "info", "admin" };

    public virtual TKey Id { get; set; }

    /*
    [RegularExpression(@"[a-z0-9\-]+")]
    [StringLength(16, MinimumLength = 6)]
    public virtual string TenantName { get; set; }
    */

    public virtual ICollection<TenantUser<TKey>> Users { get; set; } = new HashSet<TenantUser<TKey>>();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      /*
      if (ReservedTenantNames.Contains(TenantName))
        yield return new ValidationResult($"The tenant name {TenantName} is reserved.");
      */

      yield return ValidationResult.Success;
    }
  }
}
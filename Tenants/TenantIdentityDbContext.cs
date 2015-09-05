using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Microsoft.AspNet.Identity.EntityFramework.Tenant
{
  public class TenantIdentityDbContext<TUser, TRole, TTenant>
    : TenantIdentityDbContext<TUser, TRole, TTenant, string, TenantUserLogin, IdentityUserRole<string>, IdentityUserClaim<string>>
    where TUser : TenantUser
    where TRole : IdentityRole<string>
    where TTenant : Tenant
  {
  }

  public class TenantIdentityDbContext<TUser, TRole, TTenant, TKey, TUserLogin, TUserRole, TUserClaim>
    : IdentityDbContext<TUser, TRole, TKey>
    where TUser : TenantUser<TKey>
    where TRole : IdentityRole<TKey>
    where TTenant : Tenant<TKey>
    where TKey : IEquatable<TKey>
    where TUserLogin : TenantUserLogin<TKey>, new()
  {
    public DbSet<TTenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      //hack to avoid duplicate generations of the following
      builder.Model.RemoveEntityType(builder.Entity<IdentityUser<TKey>>().Metadata);
      builder.Model.RemoveEntityType(builder.Entity<IdentityUserLogin<TKey>>().Metadata);

      //builder.Entity<TenantUser<TKey>>().BaseType<IdentityUser<TKey>>();
      //builder.Entity<TenantUserLogin<TKey>>().BaseType<IdentityUserLogin<TKey>>();
      builder.Entity<TenantUser>().BaseType<TenantUser<string>>();
      builder.Entity<Tenant>().BaseType<Tenant<string>>();

      builder.Entity<TUser>().BaseType<TenantUser<TKey>>();
      builder.Entity<TTenant>().BaseType<Tenant<TKey>>();

      //builder.Entity<TTenant>().BaseType<TTenant>();
      //builder.Entity<TenantUser<string>>().BaseType<TUser>();

      builder.Entity<TenantUser<TKey>>(b =>
      {
        b.Key(u => u.Id).Name("PK_TenantUser");
        b.Index(u => u.NormalizedUserName).Unique().Name("UserNameIndex");
        b.Index(u => u.NormalizedEmail).Name("EmailIndex");
        b.Index(u => new { u.UserName, u.TenantId }).Unique().Name("UserTenantIndex");
        b.ToTable("Users");

        b.Property(u => u.ConcurrencyStamp).ConcurrencyToken();

        b.Property(u => u.UserName).MaxLength(16);
        b.Property(u => u.NormalizedUserName).MaxLength(16);

        b.Property(u => u.Email).MaxLength(256);
        b.Property(u => u.NormalizedEmail).MaxLength(256);

        b.Property(u => u.TenantId).Required();

        //b.Collection(u => u.Claims).InverseReference().ForeignKey(uc => uc.UserId);
        //b.Collection(u => u.Roles).InverseReference().ForeignKey(ur => ur.UserId);

        //b.Reference(u => u.Tenant);
      });

      builder.Entity<TRole>(b =>
      {
        b.Key(r => r.Id).Name("PK_Role");
        b.Index(r => r.NormalizedName).Name("RoleNameIndex");
        b.ToTable("Roles");
        b.Property(r => r.ConcurrencyStamp).ConcurrencyToken();

        b.Property(u => u.Name).MaxLength(256);
        b.Property(u => u.NormalizedName).MaxLength(256);

        //b.Collection(r => r.Users).InverseReference().ForeignKey(ur => ur.RoleId);
        //b.Collection(r => r.Claims).InverseReference().ForeignKey(rc => rc.RoleId);
      });

      builder.Entity<Tenant<TKey>>(b =>
      {
        b.Key(t => t.Id).Name("PK_Tenant");
        //b.Index(t => t.TenantName).Unique().IndexName("TenantNameIndex");
        b.ToTable("Tenants");

        //b.Collection(t => t.Users).InverseReference().ForeignKey(u => u.TenantId);
      });

      builder.Entity<IdentityUserClaim<TKey>>(b =>
      {
        b.Key(uc => uc.Id).Name("PK_UserClaim");
        b.ToTable("Claims");
      });

      builder.Entity<TUserLogin>(b =>
      {
        b.Key(ul =>
        new
        {
          ul.LoginProvider,
          ul.ProviderKey,
          ul.UserId,
          ul.TenantId,
        }).Name("PK_Login");
        b.ToTable("Logins");
      });

      builder.Entity<IdentityRoleClaim<TKey>>(b =>
      {
        b.Key(rc => rc.Id).Name("PK_RoleClaim");
        b.ToTable("RoleClaims");
      });

      builder.Entity<IdentityUserRole<TKey>>(b =>
      {
        b.Key(r => new { r.UserId, r.RoleId }).Name("PK_UserRole");
        b.ToTable("UserRoles");
      });
      // Blocks delete currently without cascade
      //.ForeignKeys(fk => fk.ForeignKey<TUser>(f => f.UserId))
      //.ForeignKeys(fk => fk.ForeignKey<TRole>(f => f.RoleId)); 
    }
  }
}

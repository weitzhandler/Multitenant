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

      builder.Entity<TUser>().BaseType<TenantUser<TKey>>();
      builder.Entity<TTenant>().BaseType<Tenant<TKey>>();

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

      b.Collection(u => u.Claims).InverseReference().ForeignKey(uc => uc.UserId).ConstraintName("FK_User_Claims");
      b.Collection(u => u.Roles).InverseReference().ForeignKey(ur => ur.UserId).ConstraintName("FK_User_Roles");

      b.Reference(u => u.Tenant)
       .InverseCollection(t => t.Users)
       .ForeignKey(tu => tu.TenantId)
       .ConstraintName("FK_User_Tenant");
    });

      builder.Entity<TRole>(b =>
      {
        b.Key(r => r.Id).Name("PK_Role");
        b.Index(r => r.Name).Name("RoleNameIndex");
        b.ToTable("Roles");
        b.Property(r => r.ConcurrencyStamp).ConcurrencyToken();

        b.Property(u => u.Name).MaxLength(256);
        b.Property(u => u.NormalizedName).MaxLength(256);

        b.Collection(r => r.Users)
         .InverseReference()
         .ForeignKey(ur => ur.RoleId)
         .ConstraintName("FK_Role_Users");

        b.Collection(r => r.Claims)
         .InverseReference()
         .ForeignKey(rc => rc.RoleId)
         .ConstraintName("FK_Role_Claims");
      });

    builder.Entity<Tenant<TKey>>(b =>
    {
      b.Key(t => t.Id).Name("PK_Tenant");
      b.ToTable("Tenants");

      b.Collection(t => t.Users)
       .InverseReference(u => u.Tenant)
       .ForeignKey(u => u.TenantId)
       .ConstraintName("FK_Tenant_TenantUser");
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

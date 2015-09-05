using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using App.Models;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace App.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextMigration
    {
        public override string Id
        {
            get { return "20150905211951_ApplicationDbContextMigration"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("App.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .ConcurrencyToken();

                    b.Property<string>("Name")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .Annotation("MaxLength", 256);

                    b.Key("Id")
                        .Annotation("Relational:Name", "PK_Role");

                    b.Index("NormalizedName")
                        .Annotation("Relational:Name", "RoleNameIndex");

                    b.Annotation("Relational:TableName", "Roles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId");

                    b.Key("Id")
                        .Annotation("Relational:Name", "PK_RoleClaim");

                    b.Annotation("Relational:TableName", "RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.Key("Id")
                        .Annotation("Relational:Name", "PK_UserClaim");

                    b.Annotation("Relational:TableName", "Claims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Key("UserId", "RoleId")
                        .Annotation("Relational:Name", "PK_UserRole");

                    b.Annotation("Relational:TableName", "UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.Tenant.Tenant<string>", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Discriminator")
                        .Required();

                    b.Key("Id")
                        .Annotation("Relational:Name", "PK_Tenant");

                    b.Annotation("Relational:DiscriminatorProperty", "Discriminator");

                    b.Annotation("Relational:DiscriminatorValue", "Tenant<string>");

                    b.Annotation("Relational:TableName", "Tenants");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.Tenant.TenantUser<string>", b =>
                {
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .ConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .Required();

                    b.Property<string>("Email")
                        .Annotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .Annotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .Annotation("MaxLength", 16);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("TenantId")
                        .Required();

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .Annotation("MaxLength", 16);

                    b.Key("Id")
                        .Annotation("Relational:Name", "PK_TenantUser");

                    b.Index("NormalizedEmail")
                        .Annotation("Relational:Name", "EmailIndex");

                    b.Index("NormalizedUserName")
                        .Unique()
                        .Annotation("Relational:Name", "UserNameIndex");

                    b.Index("UserName", "TenantId")
                        .Unique()
                        .Annotation("Relational:Name", "UserTenantIndex");

                    b.Annotation("Relational:DiscriminatorProperty", "Discriminator");

                    b.Annotation("Relational:DiscriminatorValue", "TenantUser<string>");

                    b.Annotation("Relational:TableName", "Users");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.Tenant.TenantUserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("UserId");

                    b.Property<string>("TenantId");

                    b.Property<string>("ProviderDisplayName");

                    b.Key("LoginProvider", "ProviderKey", "UserId", "TenantId")
                        .Annotation("Relational:Name", "PK_Login");

                    b.Annotation("Relational:TableName", "Logins");
                });

            modelBuilder.Entity("App.Models.ApplicationTenant", b =>
                {
                    b.BaseType("Microsoft.AspNet.Identity.EntityFramework.Tenant.Tenant<string>");


                    b.Annotation("Relational:DiscriminatorValue", "ApplicationTenant");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.Tenant.Tenant", b =>
                {
                    b.BaseType("Microsoft.AspNet.Identity.EntityFramework.Tenant.Tenant<string>");


                    b.Annotation("Relational:DiscriminatorValue", "Tenant");
                });

            modelBuilder.Entity("App.Models.ApplicationUser", b =>
                {
                    b.BaseType("Microsoft.AspNet.Identity.EntityFramework.Tenant.TenantUser<string>");


                    b.Annotation("Relational:DiscriminatorValue", "ApplicationUser");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.Tenant.TenantUser", b =>
                {
                    b.BaseType("Microsoft.AspNet.Identity.EntityFramework.Tenant.TenantUser<string>");


                    b.Annotation("Relational:DiscriminatorValue", "TenantUser");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.Tenant.TenantUser<string>", b =>
                {
                    b.Reference("Microsoft.AspNet.Identity.EntityFramework.Tenant.Tenant<string>")
                        .InverseCollection()
                        .ForeignKey("TenantId");
                });

            modelBuilder.Entity("App.Models.ApplicationUser", b =>
                {
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.Tenant.TenantUser", b =>
                {
                });
        }
    }
}

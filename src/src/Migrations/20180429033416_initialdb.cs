using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace src.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FullName = table.Column<string>(maxLength: 100, nullable: true),
                    IsCustomer = table.Column<bool>(nullable: false),
                    IsSuperAdmin = table.Column<bool>(nullable: false),
                    IsSupportAgent = table.Column<bool>(nullable: false),
                    IsSupportEngineer = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    ProfilePictureUrl = table.Column<string>(maxLength: 250, nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    WallpaperPictureUrl = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    organizationId = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    description = table.Column<string>(maxLength: 200, nullable: true),
                    organizationName = table.Column<string>(maxLength: 100, nullable: false),
                    organizationOwnerId = table.Column<string>(nullable: true),
                    thumbUrl = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.organizationId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    customerId = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    address = table.Column<string>(maxLength: 100, nullable: true),
                    customerName = table.Column<string>(maxLength: 100, nullable: false),
                    customerType = table.Column<int>(nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    linkedin = table.Column<string>(maxLength: 100, nullable: true),
                    organizationId = table.Column<Guid>(nullable: false),
                    phone = table.Column<string>(maxLength: 20, nullable: true),
                    thumbUrl = table.Column<string>(maxLength: 255, nullable: true),
                    website = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.customerId);
                    table.ForeignKey(
                        name: "FK_Customer_Organization_organizationId",
                        column: x => x.organizationId,
                        principalTable: "Organization",
                        principalColumn: "organizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    productId = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    description = table.Column<string>(maxLength: 200, nullable: true),
                    organizationId = table.Column<Guid>(nullable: false),
                    productCategory = table.Column<int>(nullable: false),
                    productName = table.Column<string>(maxLength: 100, nullable: false),
                    thumbUrl = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.productId);
                    table.ForeignKey(
                        name: "FK_Product_Organization_organizationId",
                        column: x => x.organizationId,
                        principalTable: "Organization",
                        principalColumn: "organizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportAgent",
                columns: table => new
                {
                    supportAgentId = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    applicationUserId = table.Column<string>(nullable: true),
                    organizationId = table.Column<Guid>(nullable: false),
                    supportAgentName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportAgent", x => x.supportAgentId);
                    table.ForeignKey(
                        name: "FK_SupportAgent_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportAgent_Organization_organizationId",
                        column: x => x.organizationId,
                        principalTable: "Organization",
                        principalColumn: "organizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportEngineer",
                columns: table => new
                {
                    supportEngineerId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    applicationUserId = table.Column<string>(nullable: true),
                    organizationId = table.Column<Guid>(nullable: false),
                    supportEngineerName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportEngineer", x => x.supportEngineerId);
                    table.ForeignKey(
                        name: "FK_SupportEngineer_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupportEngineer_Organization_organizationId",
                        column: x => x.organizationId,
                        principalTable: "Organization",
                        principalColumn: "organizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    ticketId = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    contactId = table.Column<Guid>(nullable: false),
                    customerId = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: true),
                    organizationId = table.Column<Guid>(nullable: false),
                    phone = table.Column<string>(maxLength: 20, nullable: true),
                    productId = table.Column<Guid>(nullable: false),
                    supportAgentId = table.Column<Guid>(nullable: false),
                    supportEngineerId = table.Column<Guid>(nullable: false),
                    ticketChannel = table.Column<int>(nullable: false),
                    ticketName = table.Column<string>(maxLength: 100, nullable: false),
                    ticketPriority = table.Column<int>(nullable: false),
                    ticketStatus = table.Column<int>(nullable: false),
                    ticketType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.ticketId);
                    table.ForeignKey(
                        name: "FK_Ticket_Organization_organizationId",
                        column: x => x.organizationId,
                        principalTable: "Organization",
                        principalColumn: "organizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    contactId = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    applicationUserId = table.Column<string>(nullable: true),
                    contactName = table.Column<string>(maxLength: 100, nullable: false),
                    customerId = table.Column<Guid>(nullable: false),
                    description = table.Column<string>(maxLength: 200, nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: false),
                    linkedin = table.Column<string>(maxLength: 100, nullable: true),
                    phone = table.Column<string>(maxLength: 20, nullable: true),
                    secondaryEmail = table.Column<string>(maxLength: 100, nullable: true),
                    thumbUrl = table.Column<string>(maxLength: 255, nullable: true),
                    website = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.contactId);
                    table.ForeignKey(
                        name: "FK_Contact_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contact_Customer_customerId",
                        column: x => x.customerId,
                        principalTable: "Customer",
                        principalColumn: "customerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_applicationUserId",
                table: "Contact",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_customerId",
                table: "Contact",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_organizationId",
                table: "Customer",
                column: "organizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_organizationId",
                table: "Product",
                column: "organizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportAgent_applicationUserId",
                table: "SupportAgent",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportAgent_organizationId",
                table: "SupportAgent",
                column: "organizationId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportEngineer_applicationUserId",
                table: "SupportEngineer",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportEngineer_organizationId",
                table: "SupportEngineer",
                column: "organizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_organizationId",
                table: "Ticket",
                column: "organizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "SupportAgent");

            migrationBuilder.DropTable(
                name: "SupportEngineer");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}

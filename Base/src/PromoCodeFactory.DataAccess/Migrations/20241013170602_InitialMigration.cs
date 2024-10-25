using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCodeFactory.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    NumberIssuedPromoCodes = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.PartnerId);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    PreferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.PreferenceId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    Description = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "PartnerPromoCodeLimits",
                columns: table => new
                {
                    PartnerPromocodeLimitId = table.Column<Guid>(type: "uuid", nullable: false),
                    Limit = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerPromoCodeLimits", x => x.PartnerPromocodeLimitId);
                    table.ForeignKey(
                        name: "FK_PartnerPromoCodeLimits_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "PartnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerPreference",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreferenceId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerPreference", x => new { x.CustomerId, x.PreferenceId });
                    table.ForeignKey(
                        name: "FK_CustomerPreference_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerPreference_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "PreferenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Email = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    AppliedPromocodesCount = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PromoCodes",
                columns: table => new
                {
                    PromoCodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ServiceInfo = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PartnerName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PreferenceId = table.Column<Guid>(type: "uuid", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCodes", x => x.PromoCodeId);
                    table.ForeignKey(
                        name: "FK_PromoCodes_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoCodes_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PromoCodes_Preferences_PreferenceId",
                        column: x => x.PreferenceId,
                        principalTable: "Preferences",
                        principalColumn: "PreferenceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerPreference_PreferenceId",
                table: "CustomerPreference",
                column: "PreferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPromoCodeLimits_PartnerId",
                table: "PartnerPromoCodeLimits",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_CustomerId",
                table: "PromoCodes",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_EmployeeId",
                table: "PromoCodes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCodes_PreferenceId",
                table: "PromoCodes",
                column: "PreferenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerPreference");

            migrationBuilder.DropTable(
                name: "PartnerPromoCodeLimits");

            migrationBuilder.DropTable(
                name: "PromoCodes");

            migrationBuilder.DropTable(
                name: "Partners");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

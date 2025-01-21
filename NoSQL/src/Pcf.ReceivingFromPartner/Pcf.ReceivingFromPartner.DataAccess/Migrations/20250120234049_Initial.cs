using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pcf.ReceivingFromPartner.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "PartnerPromoCodeLimits",
                columns: table => new
                {
                    PartnerPromocodeLimitId = table.Column<Guid>(type: "uuid", nullable: false),
                    Limit = table.Column<int>(type: "integer", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
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
                name: "PromoCode",
                columns: table => new
                {
                    PromoCodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    ServiceInfo = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    BeginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PartnerManagerId = table.Column<Guid>(type: "uuid", nullable: true),
                    PreferenceId = table.Column<int>(type: "integer", nullable: false),
                    PreferenceName = table.Column<string>(type: "text", nullable: true),
                    PartnerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PromoCode", x => x.PromoCodeId);
                    table.ForeignKey(
                        name: "FK_PromoCode_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "PartnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPromoCodeLimits_PartnerId",
                table: "PartnerPromoCodeLimits",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PromoCode_PartnerId",
                table: "PromoCode",
                column: "PartnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerPromoCodeLimits");

            migrationBuilder.DropTable(
                name: "PromoCode");

            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}

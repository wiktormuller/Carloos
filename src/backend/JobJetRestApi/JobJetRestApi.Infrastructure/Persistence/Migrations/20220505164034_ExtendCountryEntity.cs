using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobJetRestApi.Infrastructure.Persistence.Migrations
{
    public partial class ExtendCountryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Addresses_AddressId",
                table: "JobOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_TechnologyTypes_TechnologyTypeId",
                table: "JobOffers");

            migrationBuilder.DropIndex(
                name: "IX_JobOffers_TechnologyTypeId",
                table: "JobOffers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "TechnologyTypeId",
                table: "JobOffers");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "Address");

            migrationBuilder.RenameIndex(
                name: "IX_Addresses_CountryId",
                table: "Address",
                newName: "IX_Address_CountryId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "JobOffers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "JobOffers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "LatitudeOfCapital",
                table: "Countries",
                type: "decimal(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "LongitudeOfCapital",
                table: "Countries",
                type: "decimal(9,6)",
                precision: 9,
                scale: 6,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Address",
                table: "Address",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "JobOfferTechnologyType",
                columns: table => new
                {
                    JobOffersId = table.Column<int>(type: "int", nullable: false),
                    TechnologyTypesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobOfferTechnologyType", x => new { x.JobOffersId, x.TechnologyTypesId });
                    table.ForeignKey(
                        name: "FK_JobOfferTechnologyType_JobOffers_JobOffersId",
                        column: x => x.JobOffersId,
                        principalTable: "JobOffers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobOfferTechnologyType_TechnologyTypes_TechnologyTypesId",
                        column: x => x.TechnologyTypesId,
                        principalTable: "TechnologyTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOfferTechnologyType_TechnologyTypesId",
                table: "JobOfferTechnologyType",
                column: "TechnologyTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Countries_CountryId",
                table: "Address",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Address_AddressId",
                table: "JobOffers",
                column: "AddressId",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Countries_CountryId",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Address_AddressId",
                table: "JobOffers");

            migrationBuilder.DropTable(
                name: "JobOfferTechnologyType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Address",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "JobOffers");

            migrationBuilder.DropColumn(
                name: "LatitudeOfCapital",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "LongitudeOfCapital",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Address",
                newName: "Addresses");

            migrationBuilder.RenameIndex(
                name: "IX_Address_CountryId",
                table: "Addresses",
                newName: "IX_Addresses_CountryId");

            migrationBuilder.AddColumn<int>(
                name: "TechnologyTypeId",
                table: "JobOffers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JobOffers_TechnologyTypeId",
                table: "JobOffers",
                column: "TechnologyTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Countries_CountryId",
                table: "Addresses",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Addresses_AddressId",
                table: "JobOffers",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_TechnologyTypes_TechnologyTypeId",
                table: "JobOffers",
                column: "TechnologyTypeId",
                principalTable: "TechnologyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

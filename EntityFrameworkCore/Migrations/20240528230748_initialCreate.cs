using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CaseTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mechanical = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Electricity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RepairActivitys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcceptanceDateOfTheVehicle = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceEntryKm = table.Column<int>(type: "int", nullable: true),
                    PersonWhoBroughtTheVehicle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VehicleMalfunctionDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PersonnelReceivingTheVehicle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PlannedEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChangingPart = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ChangedPartPhoto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AmountOfChangedParts = table.Column<double>(type: "float", nullable: true),
                    AmountOfOutsourcedLabor = table.Column<double>(type: "float", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairActivitys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockCardBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCardBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockCardUnits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCardUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CookieTimeout = table.Column<int>(type: "int", nullable: false),
                    EMail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    GsmNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RefreshToken = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RefreshTokenExpireIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserRightsView = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", maxLength: 1024000, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutSourceLabors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RepairActivityId = table.Column<int>(type: "int", nullable: false),
                    LaborName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PersonName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PersonSurname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutSourceLabors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OutSourceLabors_RepairActivitys_RepairActivityId",
                        column: x => x.RepairActivityId,
                        principalTable: "RepairActivitys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false),
                    NameOfThePurchasingCompanyId = table.Column<int>(type: "int", nullable: true),
                    StockCardBrandId = table.Column<int>(type: "int", nullable: false),
                    StockCardUnitId = table.Column<int>(type: "int", nullable: false),
                    StockCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SpecialCode = table.Column<int>(type: "int", nullable: true),
                    ManufacturerCode = table.Column<int>(type: "int", nullable: true),
                    ShelfNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BoxNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PurchasePrice = table.Column<double>(type: "float", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<double>(type: "float", nullable: true),
                    PacanianPhoto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockCards_Firms_NameOfThePurchasingCompanyId",
                        column: x => x.NameOfThePurchasingCompanyId,
                        principalTable: "Firms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StockCards_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "ProductGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockCards_StockCardBrands_StockCardBrandId",
                        column: x => x.StockCardBrandId,
                        principalTable: "StockCardBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockCards_StockCardUnits_StockCardUnitId",
                        column: x => x.StockCardUnitId,
                        principalTable: "StockCardUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleBrandId = table.Column<int>(type: "int", nullable: false),
                    VehicleTypeId = table.Column<int>(type: "int", nullable: false),
                    CaseTypeId = table.Column<int>(type: "int", nullable: false),
                    LicensePlateNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ModelYear = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Engine = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Chassis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    License = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Chance = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ColorKeyCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RadioCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EngineDisplacementPowerKW = table.Column<int>(type: "int", nullable: true),
                    FuelTypeId = table.Column<int>(type: "int", nullable: true),
                    NetWeight = table.Column<int>(type: "int", nullable: true),
                    WarrantyEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfLastMedicalInspection = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FenniMuayeneEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastExhaustInspectionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrafficInsuranceStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrafficInsuranceEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsuranceStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InsuranceEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UnitUsingTheTool = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhotoOfTheVehicle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_CaseTypes_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalTable: "CaseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleBrands_VehicleBrandId",
                        column: x => x.VehicleBrandId,
                        principalTable: "VehicleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleTypes_VehicleTypeId",
                        column: x => x.VehicleTypeId,
                        principalTable: "VehicleTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    StockCardId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MovementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    StockMovementType = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockMovements_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockMovements_StockCards_StockCardId",
                        column: x => x.StockCardId,
                        principalTable: "StockCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OutSourceLabors_RepairActivityId",
                table: "OutSourceLabors",
                column: "RepairActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_NameOfThePurchasingCompanyId",
                table: "StockCards",
                column: "NameOfThePurchasingCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_ProductGroupId",
                table: "StockCards",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_StockCardBrandId",
                table: "StockCards",
                column: "StockCardBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_StockCards_StockCardUnitId",
                table: "StockCards",
                column: "StockCardUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_ManufacturerId",
                table: "StockMovements",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_StockMovements_StockCardId",
                table: "StockMovements",
                column: "StockCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_CaseTypeId",
                table: "Vehicles",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleBrandId",
                table: "Vehicles",
                column: "VehicleBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleTypeId",
                table: "Vehicles",
                column: "VehicleTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutSourceLabors");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "StockMovements");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "RepairActivitys");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "StockCards");

            migrationBuilder.DropTable(
                name: "CaseTypes");

            migrationBuilder.DropTable(
                name: "VehicleBrands");

            migrationBuilder.DropTable(
                name: "VehicleTypes");

            migrationBuilder.DropTable(
                name: "Firms");

            migrationBuilder.DropTable(
                name: "ProductGroups");

            migrationBuilder.DropTable(
                name: "StockCardBrands");

            migrationBuilder.DropTable(
                name: "StockCardUnits");
        }
    }
}

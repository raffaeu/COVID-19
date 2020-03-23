using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Data.Migrations
{
    public partial class RefactorSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_CONFIRMED");

            migrationBuilder.DropTable(
                name: "TBL_DEAD");

            migrationBuilder.DropTable(
                name: "TBL_RECOVERED");

            migrationBuilder.CreateTable(
                name: "TBL_RAW_DATA",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    ConfirmedCount = table.Column<int>(nullable: false),
                    RecoveredCount = table.Column<int>(nullable: false),
                    DeadCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_RAW_DATA", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RAW_DATA_COUNTRY",
                table: "TBL_RAW_DATA",
                column: "Country")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_RAW_DATA_DATE",
                table: "TBL_RAW_DATA",
                column: "Date")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_RAW_DATA_STATE",
                table: "TBL_RAW_DATA",
                column: "State")
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_RAW_DATA");

            migrationBuilder.CreateTable(
                name: "TBL_CONFIRMED",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CONFIRMED", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_DEAD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_DEAD", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_RECOVERED",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_RECOVERED", x => x.Id);
                });
        }
    }
}

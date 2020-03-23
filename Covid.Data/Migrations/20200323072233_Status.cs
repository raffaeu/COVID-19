using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Covid.Data.Migrations
{
    public partial class Status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_IMPORT_STATUS",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ImportDate = table.Column<DateTime>(nullable: false),
                    Records = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_IMPORT_STATUS", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_IMPORT_STATUS");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.ServicesManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateByClientIp",
                table: "SubSubServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByUserId",
                table: "SubSubServices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "SubSubServices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateByClientIp",
                table: "SubSubServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByUserId",
                table: "SubSubServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "SubSubServices",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomAutoHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Changed = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomAutoHistory", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomAutoHistory");

            migrationBuilder.DropColumn(
                name: "CreateByClientIp",
                table: "SubSubServices");

            migrationBuilder.DropColumn(
                name: "CreateByUserId",
                table: "SubSubServices");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "SubSubServices");

            migrationBuilder.DropColumn(
                name: "UpdateByClientIp",
                table: "SubSubServices");

            migrationBuilder.DropColumn(
                name: "UpdateByUserId",
                table: "SubSubServices");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "SubSubServices");
        }
    }
}

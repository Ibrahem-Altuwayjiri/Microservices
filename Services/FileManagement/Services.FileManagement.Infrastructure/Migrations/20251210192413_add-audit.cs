using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.FileManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addaudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "DownloaderInfo",
                newName: "CreateByUserId");

            migrationBuilder.RenameColumn(
                name: "DownloadDate",
                table: "DownloaderInfo",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "ClientIp",
                table: "DownloaderInfo",
                newName: "CreateByClientIp");

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

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "DownloaderInfo",
                newName: "DownloadDate");

            migrationBuilder.RenameColumn(
                name: "CreateByUserId",
                table: "DownloaderInfo",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreateByClientIp",
                table: "DownloaderInfo",
                newName: "ClientIp");
        }
    }
}

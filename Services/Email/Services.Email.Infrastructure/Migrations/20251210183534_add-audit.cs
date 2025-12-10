using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Email.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addaudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDtae",
                table: "TemplateDetails",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "CreateBy",
                table: "TemplateDetails",
                newName: "CreateByUserId");

            migrationBuilder.AddColumn<string>(
                name: "CreateByClientIp",
                table: "TemplateDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UpdateByClientIp",
                table: "TemplateDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByUserId",
                table: "TemplateDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "TemplateDetails",
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
                table: "TemplateDetails");

            migrationBuilder.DropColumn(
                name: "UpdateByClientIp",
                table: "TemplateDetails");

            migrationBuilder.DropColumn(
                name: "UpdateByUserId",
                table: "TemplateDetails");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "TemplateDetails");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "TemplateDetails",
                newName: "CreateDtae");

            migrationBuilder.RenameColumn(
                name: "CreateByUserId",
                table: "TemplateDetails",
                newName: "CreateBy");
        }
    }
}

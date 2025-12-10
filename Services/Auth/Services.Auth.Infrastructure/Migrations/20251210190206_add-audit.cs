using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addaudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtpDetails_AspNetUsers_UserId",
                table: "OtpDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenDetails_AspNetUsers_UserId",
                table: "TokenDetails");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "TokenDetails",
                newName: "CreateByUserId");

            migrationBuilder.RenameColumn(
                name: "CreateByDeviceIP",
                table: "TokenDetails",
                newName: "CreateByClientIp");

            migrationBuilder.RenameIndex(
                name: "IX_TokenDetails_UserId",
                table: "TokenDetails",
                newName: "IX_TokenDetails_CreateByUserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "OtpDetails",
                newName: "CreateByUserId");

            migrationBuilder.RenameColumn(
                name: "RequestIp",
                table: "OtpDetails",
                newName: "CreateByClientIp");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "OtpDetails",
                newName: "CreateDate");

            migrationBuilder.RenameIndex(
                name: "IX_OtpDetails_UserId",
                table: "OtpDetails",
                newName: "IX_OtpDetails_CreateByUserId");

            migrationBuilder.RenameColumn(
                name: "CraeteDate",
                table: "AspNetUsers",
                newName: "UpdateDate");

            migrationBuilder.AddColumn<string>(
                name: "UpdateByClientIp",
                table: "TokenDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByUserId",
                table: "TokenDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "TokenDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByClientIp",
                table: "OtpDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByUserId",
                table: "OtpDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "OtpDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreateByClientIp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreateByUserId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdateByClientIp",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdateByUserId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
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

            migrationBuilder.AddForeignKey(
                name: "FK_OtpDetails_AspNetUsers_CreateByUserId",
                table: "OtpDetails",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenDetails_AspNetUsers_CreateByUserId",
                table: "TokenDetails",
                column: "CreateByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OtpDetails_AspNetUsers_CreateByUserId",
                table: "OtpDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TokenDetails_AspNetUsers_CreateByUserId",
                table: "TokenDetails");

            migrationBuilder.DropTable(
                name: "CustomAutoHistory");

            migrationBuilder.DropColumn(
                name: "UpdateByClientIp",
                table: "TokenDetails");

            migrationBuilder.DropColumn(
                name: "UpdateByUserId",
                table: "TokenDetails");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "TokenDetails");

            migrationBuilder.DropColumn(
                name: "UpdateByClientIp",
                table: "OtpDetails");

            migrationBuilder.DropColumn(
                name: "UpdateByUserId",
                table: "OtpDetails");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "OtpDetails");

            migrationBuilder.DropColumn(
                name: "CreateByClientIp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateByUserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdateByClientIp",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdateByUserId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CreateByUserId",
                table: "TokenDetails",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreateByClientIp",
                table: "TokenDetails",
                newName: "CreateByDeviceIP");

            migrationBuilder.RenameIndex(
                name: "IX_TokenDetails_CreateByUserId",
                table: "TokenDetails",
                newName: "IX_TokenDetails_UserId");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "OtpDetails",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreateByUserId",
                table: "OtpDetails",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreateByClientIp",
                table: "OtpDetails",
                newName: "RequestIp");

            migrationBuilder.RenameIndex(
                name: "IX_OtpDetails_CreateByUserId",
                table: "OtpDetails",
                newName: "IX_OtpDetails_UserId");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "AspNetUsers",
                newName: "CraeteDate");

            migrationBuilder.AddForeignKey(
                name: "FK_OtpDetails_AspNetUsers_UserId",
                table: "OtpDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TokenDetails_AspNetUsers_UserId",
                table: "TokenDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

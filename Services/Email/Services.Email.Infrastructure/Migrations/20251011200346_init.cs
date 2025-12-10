using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Email.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailContent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Footer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecipientType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipientType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SenderInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientIp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SenderInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SenderInfoId = table.Column<int>(type: "int", nullable: false),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    EmailContentId = table.Column<int>(type: "int", nullable: false),
                    IsSend = table.Column<bool>(type: "bit", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TryNum = table.Column<int>(type: "int", nullable: false),
                    LastTrySend = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailDetails_EmailContent_EmailContentId",
                        column: x => x.EmailContentId,
                        principalTable: "EmailContent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailDetails_SenderInfo_SenderInfoId",
                        column: x => x.SenderInfoId,
                        principalTable: "SenderInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailDetails_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TemplateDetails",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    VersionNumber = table.Column<int>(type: "int", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDtae = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeaderImg = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    SubHeaderImg = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    TitleColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstLineColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLineColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdLineColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubFooterImg = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FooterImg = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateDetails", x => new { x.TemplateId, x.VersionNumber });
                    table.ForeignKey(
                        name: "FK_TemplateDetails_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extensions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_EmailDetails_EmailDetailsId",
                        column: x => x.EmailDetailsId,
                        principalTable: "EmailDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailErrorLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailDetailsId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailErrorLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailErrorLog_EmailDetails_EmailDetailsId",
                        column: x => x.EmailDetailsId,
                        principalTable: "EmailDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailRecipient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientTypeId = table.Column<int>(type: "int", nullable: false),
                    EmailDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailRecipient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailRecipient_EmailDetails_EmailDetailsId",
                        column: x => x.EmailDetailsId,
                        principalTable: "EmailDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailRecipient_RecipientType_RecipientTypeId",
                        column: x => x.RecipientTypeId,
                        principalTable: "RecipientType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EmailDetailsId",
                table: "Attachments",
                column: "EmailDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDetails_EmailContentId",
                table: "EmailDetails",
                column: "EmailContentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDetails_SenderInfoId",
                table: "EmailDetails",
                column: "SenderInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDetails_TemplateId",
                table: "EmailDetails",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailErrorLog_EmailDetailsId",
                table: "EmailErrorLog",
                column: "EmailDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailRecipient_EmailDetailsId",
                table: "EmailRecipient",
                column: "EmailDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailRecipient_RecipientTypeId",
                table: "EmailRecipient",
                column: "RecipientTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "EmailErrorLog");

            migrationBuilder.DropTable(
                name: "EmailRecipient");

            migrationBuilder.DropTable(
                name: "TemplateDetails");

            migrationBuilder.DropTable(
                name: "EmailDetails");

            migrationBuilder.DropTable(
                name: "RecipientType");

            migrationBuilder.DropTable(
                name: "EmailContent");

            migrationBuilder.DropTable(
                name: "SenderInfo");

            migrationBuilder.DropTable(
                name: "Template");
        }
    }
}

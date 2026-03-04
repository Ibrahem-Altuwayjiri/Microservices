using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.ServicesManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addServiceDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "ServiceDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Prerequisites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDetailsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prerequisites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prerequisites_ServiceDetails_ServiceDetailsId",
                        column: x => x.ServiceDetailsId,
                        principalTable: "ServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDetailsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequiredDocuments_ServiceDetails_ServiceDetailsId",
                        column: x => x.ServiceDetailsId,
                        principalTable: "ServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceDetailsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Steps_ServiceDetails_ServiceDetailsId",
                        column: x => x.ServiceDetailsId,
                        principalTable: "ServiceDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prerequisites_ServiceDetailsId",
                table: "Prerequisites",
                column: "ServiceDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredDocuments_ServiceDetailsId",
                table: "RequiredDocuments",
                column: "ServiceDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ServiceDetailsId",
                table: "Steps",
                column: "ServiceDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prerequisites");

            migrationBuilder.DropTable(
                name: "RequiredDocuments");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "ServiceDetails");
        }
    }
}

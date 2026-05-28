using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class addApplicationDocuments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationDocumentRequestLog",
                schema: "Application",
                columns: table => new
                {
                    RequestID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    DocumentTypeID = table.Column<int>(type: "int", nullable: false),
                    SchoolID = table.Column<long>(type: "bigint", nullable: false),
                    RequestContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponseContentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseStatusCode = table.Column<int>(type: "int", nullable: false),
                    ResponseTimestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDocumentRequestLog", x => x.RequestID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentCategorys",
                schema: "Master",
                columns: table => new
                {
                    DocumentCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentCategorys", x => x.DocumentCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentTypes",
                schema: "Master",
                columns: table => new
                {
                    DocumentTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentCategoryID = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentTypes", x => x.DocumentTypeID);
                    table.ForeignKey(
                        name: "FK_DocumentTypes_DocumentCategorys_DocumentCategoryID",
                        column: x => x.DocumentCategoryID,
                        principalSchema: "Master",
                        principalTable: "DocumentCategorys",
                        principalColumn: "DocumentCategoryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationDocuments",
                schema: "Application",
                columns: table => new
                {
                    ApplicationDocumentID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DocumentGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentID = table.Column<long>(type: "bigint", nullable: false),
                    LoanApplicationID = table.Column<long>(type: "bigint", nullable: false),
                    DocumentTypeID = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationDocuments", x => x.ApplicationDocumentID);
                    table.ForeignKey(
                        name: "FK_ApplicationDocuments_DocumentTypes_DocumentTypeID",
                        column: x => x.DocumentTypeID,
                        principalSchema: "Master",
                        principalTable: "DocumentTypes",
                        principalColumn: "DocumentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationDocuments_LoanApplication_LoanApplicationID",
                        column: x => x.LoanApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDocuments_DocumentTypeID",
                schema: "Application",
                table: "ApplicationDocuments",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationDocuments_LoanApplicationID",
                schema: "Application",
                table: "ApplicationDocuments",
                column: "LoanApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentTypes_DocumentCategoryID",
                schema: "Master",
                table: "DocumentTypes",
                column: "DocumentCategoryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationDocumentRequestLog",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "ApplicationDocuments",
                schema: "Application");

            migrationBuilder.DropTable(
                name: "DocumentTypes",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "DocumentCategorys",
                schema: "Master");
        }
    }
}

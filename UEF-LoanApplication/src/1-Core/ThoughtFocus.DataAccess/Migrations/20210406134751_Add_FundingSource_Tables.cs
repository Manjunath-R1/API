using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class Add_FundingSource_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {  
            migrationBuilder.EnsureSchema(
                name: "FundingSource"); 

            

            migrationBuilder.CreateTable(
                name: "FundingEntities",
                schema: "FundingSource",
                columns: table => new
                {
                    FundingEntityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundingEntityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingEntities", x => x.FundingEntityID);
                });

            migrationBuilder.CreateTable(
                name: "FundingTypes",
                schema: "Master",
                columns: table => new
                {
                    FundingTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingTypes", x => x.FundingTypeID);
                });

            migrationBuilder.CreateTable(
                name: "TransactionTypes",
                schema: "Master",
                columns: table => new
                {
                    TransactionTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionTypes", x => x.TransactionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "FundingSources",
                schema: "FundingSource",
                columns: table => new
                {
                    FundingSourceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FundingEntityID = table.Column<long>(type: "bigint", nullable: false),
                    FundingTypeID = table.Column<int>(type: "int", nullable: false),
                    MinimumLoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaximumLoanAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InitialFundedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingSources", x => x.FundingSourceID);
                    table.ForeignKey(
                        name: "FK_FundingSources_FundingEntities_FundingEntityID",
                        column: x => x.FundingEntityID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingEntities",
                        principalColumn: "FundingEntityID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingSources_FundingTypes_FundingTypeID",
                        column: x => x.FundingTypeID,
                        principalSchema: "Master",
                        principalTable: "FundingTypes",
                        principalColumn: "FundingTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingSourceAndBusinessTypeMapping",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundingSourceID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessTypeID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingSourceAndBusinessTypeMapping", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundingSourceAndBusinessTypeMapping_BusinessType_BusinessTypeID",
                        column: x => x.BusinessTypeID,
                        principalSchema: "Master",
                        principalTable: "BusinessType",
                        principalColumn: "BusinessTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingSourceAndBusinessTypeMapping_FundingSources_FundingSourceID",
                        column: x => x.FundingSourceID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundingSourceAndStateMapping",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundingSourceID = table.Column<long>(type: "bigint", nullable: false),
                    StateID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundingSourceAndStateMapping", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundingSourceAndStateMapping_FundingSources_FundingSourceID",
                        column: x => x.FundingSourceID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundingSourceAndStateMapping_State_StateID",
                        column: x => x.StateID,
                        principalSchema: "Master",
                        principalTable: "State",
                        principalColumn: "StateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundTransactions",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionTypeID = table.Column<int>(type: "int", nullable: false),
                    TransactionAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FundingSourceID = table.Column<long>(type: "bigint", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationID = table.Column<long>(type: "bigint", nullable: true),
                    DateofDisbursement = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTransactions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundTransactions_FundingSources_FundingSourceID",
                        column: x => x.FundingSourceID,
                        principalSchema: "FundingSource",
                        principalTable: "FundingSources",
                        principalColumn: "FundingSourceID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FundTransactions_LoanApplication_ApplicationID",
                        column: x => x.ApplicationID,
                        principalSchema: "Application",
                        principalTable: "LoanApplication",
                        principalColumn: "LoanApplicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundTransactions_TransactionTypes_TransactionTypeID",
                        column: x => x.TransactionTypeID,
                        principalSchema: "Master",
                        principalTable: "TransactionTypes",
                        principalColumn: "TransactionTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundTransactionDocuments",
                schema: "FundingSource",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FundTransactionID = table.Column<long>(type: "bigint", nullable: false),
                    DocumentGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentTypeID = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundTransactionDocuments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FundTransactionDocuments_DocumentTypes_DocumentTypeID",
                        column: x => x.DocumentTypeID,
                        principalSchema: "Master",
                        principalTable: "DocumentTypes",
                        principalColumn: "DocumentTypeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundTransactionDocuments_FundTransactions_FundTransactionID",
                        column: x => x.FundTransactionID,
                        principalSchema: "FundingSource",
                        principalTable: "FundTransactions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });
 

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceAndBusinessTypeMapping_BusinessTypeID",
                schema: "FundingSource",
                table: "FundingSourceAndBusinessTypeMapping",
                column: "BusinessTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceAndBusinessTypeMapping_FundingSourceID",
                schema: "FundingSource",
                table: "FundingSourceAndBusinessTypeMapping",
                column: "FundingSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceAndStateMapping_FundingSourceID",
                schema: "FundingSource",
                table: "FundingSourceAndStateMapping",
                column: "FundingSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSourceAndStateMapping_StateID",
                schema: "FundingSource",
                table: "FundingSourceAndStateMapping",
                column: "StateID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSources_FundingEntityID",
                schema: "FundingSource",
                table: "FundingSources",
                column: "FundingEntityID");

            migrationBuilder.CreateIndex(
                name: "IX_FundingSources_FundingTypeID",
                schema: "FundingSource",
                table: "FundingSources",
                column: "FundingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransactionDocuments_DocumentTypeID",
                schema: "FundingSource",
                table: "FundTransactionDocuments",
                column: "DocumentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransactionDocuments_FundTransactionID",
                schema: "FundingSource",
                table: "FundTransactionDocuments",
                column: "FundTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransactions_ApplicationID",
                schema: "FundingSource",
                table: "FundTransactions",
                column: "ApplicationID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransactions_FundingSourceID",
                schema: "FundingSource",
                table: "FundTransactions",
                column: "FundingSourceID");

            migrationBuilder.CreateIndex(
                name: "IX_FundTransactions_TransactionTypeID",
                schema: "FundingSource",
                table: "FundTransactions",
                column: "TransactionTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropTable(
                name: "FundingSourceAndBusinessTypeMapping",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "FundingSourceAndStateMapping",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "FundTransactionDocuments",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "FundTransactions",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "FundingSources",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "TransactionTypes",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "FundingEntities",
                schema: "FundingSource");

            migrationBuilder.DropTable(
                name: "FundingTypes",
                schema: "Master");

          

          

         
            

            
         
 
            
        }
    }
}

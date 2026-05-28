using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class add_Notification_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Notification");

            migrationBuilder.CreateTable(
                name: "EmailTemplateHeaderFooter",
                schema: "Notification",
                columns: table => new
                {
                    EmailTemplateHeaderFooterID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentTypeID = table.Column<long>(type: "bigint", nullable: true),
                    LogoGUID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogoID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Footer = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ImageKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplateHeaderFooter", x => x.EmailTemplateHeaderFooterID);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplatePlaceholderType",
                schema: "Master",
                columns: table => new
                {
                    PlaceHolderTypeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceHolderType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplatePlaceholderType", x => x.PlaceHolderTypeID);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplatePreCondition",
                schema: "Master",
                columns: table => new
                {
                    EmailTemplatePreConditionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplatePreCondition", x => x.EmailTemplatePreConditionID);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                schema: "Master",
                columns: table => new
                {
                    NotificationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailFormat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Head = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salutation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Footer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipientType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationID);
                });

            migrationBuilder.CreateTable(
                name: "NotificationStatus",
                schema: "Master",
                columns: table => new
                {
                    NotificationStatusID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationStatus", x => x.NotificationStatusID);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowNotificationType",
                schema: "Master",
                columns: table => new
                {
                    WorkflowNotificationTypeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    WorkflowNotificationTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowNotificationType", x => x.WorkflowNotificationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplatePlaceholders",
                schema: "Notification",
                columns: table => new
                {
                    PlaceholderID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlaceHolderTypeID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplatePlaceholders", x => x.PlaceholderID);
                    table.ForeignKey(
                        name: "FK_EmailTemplatePlaceholders_EmailTemplatePlaceholderType_PlaceHolderTypeID",
                        column: x => x.PlaceHolderTypeID,
                        principalSchema: "Master",
                        principalTable: "EmailTemplatePlaceholderType",
                        principalColumn: "PlaceHolderTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailNotificationLog",
                schema: "Notification",
                columns: table => new
                {
                    EmailNotificationLogID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    To = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotificationLog", x => x.EmailNotificationLogID);
                    table.ForeignKey(
                        name: "FK_EmailNotificationLog_Notification_NotificationID",
                        column: x => x.NotificationID,
                        principalSchema: "Master",
                        principalTable: "Notification",
                        principalColumn: "NotificationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationPreCondition",
                schema: "Notification",
                columns: table => new
                {
                    NotificationPreConditionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationID = table.Column<long>(type: "bigint", nullable: false),
                    EmailTemplatePreConditionID = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPreCondition", x => x.NotificationPreConditionID);
                    table.ForeignKey(
                        name: "FK_NotificationPreCondition_EmailTemplatePreCondition_EmailTemplatePreConditionID",
                        column: x => x.EmailTemplatePreConditionID,
                        principalSchema: "Master",
                        principalTable: "EmailTemplatePreCondition",
                        principalColumn: "EmailTemplatePreConditionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationPreCondition_Notification_NotificationID",
                        column: x => x.NotificationID,
                        principalSchema: "Master",
                        principalTable: "Notification",
                        principalColumn: "NotificationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityNotification",
                schema: "Notification",
                columns: table => new
                {
                    ActivityNotificationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityID = table.Column<long>(type: "bigint", nullable: true),
                    NotificationID = table.Column<long>(type: "bigint", nullable: true),
                    WorkflowNotificationTypeID = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityNotification", x => x.ActivityNotificationID);
                    table.ForeignKey(
                        name: "FK_ActivityNotification_Notification_NotificationID",
                        column: x => x.NotificationID,
                        principalSchema: "Master",
                        principalTable: "Notification",
                        principalColumn: "NotificationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityNotification_WorkflowNotificationType_WorkflowNotificationTypeID",
                        column: x => x.WorkflowNotificationTypeID,
                        principalSchema: "Master",
                        principalTable: "WorkflowNotificationType",
                        principalColumn: "WorkflowNotificationTypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationPlaceholders",
                schema: "Notification",
                columns: table => new
                {
                    NotificationPlaceholderID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationID = table.Column<long>(type: "bigint", nullable: false),
                    PlaceholderID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPlaceholders", x => x.NotificationPlaceholderID);
                    table.ForeignKey(
                        name: "FK_NotificationPlaceholders_EmailTemplatePlaceholders_PlaceholderID",
                        column: x => x.PlaceholderID,
                        principalSchema: "Notification",
                        principalTable: "EmailTemplatePlaceholders",
                        principalColumn: "PlaceholderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationPlaceholders_Notification_NotificationID",
                        column: x => x.NotificationID,
                        principalSchema: "Master",
                        principalTable: "Notification",
                        principalColumn: "NotificationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailNotificationLogAddressee",
                schema: "Notification",
                columns: table => new
                {
                    EmailNotificationLogAddresseeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailNotificationLogID = table.Column<long>(type: "bigint", nullable: false),
                    EmailAddresses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCc = table.Column<bool>(type: "bit", nullable: false),
                    NotificationStatusID = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotificationLogAddressee", x => x.EmailNotificationLogAddresseeID);
                    table.ForeignKey(
                        name: "FK_EmailNotificationLogAddressee_EmailNotificationLog_EmailNotificationLogID",
                        column: x => x.EmailNotificationLogID,
                        principalSchema: "Notification",
                        principalTable: "EmailNotificationLog",
                        principalColumn: "EmailNotificationLogID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmailNotificationLogAddressee_NotificationStatus_NotificationStatusID",
                        column: x => x.NotificationStatusID,
                        principalSchema: "Master",
                        principalTable: "NotificationStatus",
                        principalColumn: "NotificationStatusID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotificationRecipientEmailAddress",
                schema: "Notification",
                columns: table => new
                {
                    NotificationRecipientEmailAddressID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityNotificationID = table.Column<long>(type: "bigint", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRecipientEmailAddress", x => x.NotificationRecipientEmailAddressID);
                    table.ForeignKey(
                        name: "FK_NotificationRecipientEmailAddress_ActivityNotification_ActivityNotificationID",
                        column: x => x.ActivityNotificationID,
                        principalSchema: "Notification",
                        principalTable: "ActivityNotification",
                        principalColumn: "ActivityNotificationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationRecipients",
                schema: "Notification",
                columns: table => new
                {
                    NotificationRecipientID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityNotificationID = table.Column<long>(type: "bigint", nullable: false),
                    PlaceholderID = table.Column<long>(type: "bigint", nullable: false),
                    IsTo = table.Column<bool>(type: "bit", nullable: false),
                    IsCC = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationRecipients", x => x.NotificationRecipientID);
                    table.ForeignKey(
                        name: "FK_NotificationRecipients_ActivityNotification_ActivityNotificationID",
                        column: x => x.ActivityNotificationID,
                        principalSchema: "Notification",
                        principalTable: "ActivityNotification",
                        principalColumn: "ActivityNotificationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationRecipients_EmailTemplatePlaceholders_PlaceholderID",
                        column: x => x.PlaceholderID,
                        principalSchema: "Notification",
                        principalTable: "EmailTemplatePlaceholders",
                        principalColumn: "PlaceholderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityNotification_NotificationID",
                schema: "Notification",
                table: "ActivityNotification",
                column: "NotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityNotification_WorkflowNotificationTypeID",
                schema: "Notification",
                table: "ActivityNotification",
                column: "WorkflowNotificationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotificationLog_NotificationID",
                schema: "Notification",
                table: "EmailNotificationLog",
                column: "NotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotificationLogAddressee_EmailNotificationLogID",
                schema: "Notification",
                table: "EmailNotificationLogAddressee",
                column: "EmailNotificationLogID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotificationLogAddressee_NotificationStatusID",
                schema: "Notification",
                table: "EmailNotificationLogAddressee",
                column: "NotificationStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplatePlaceholders_PlaceHolderTypeID",
                schema: "Notification",
                table: "EmailTemplatePlaceholders",
                column: "PlaceHolderTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPlaceholders_NotificationID",
                schema: "Notification",
                table: "NotificationPlaceholders",
                column: "NotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPlaceholders_PlaceholderID",
                schema: "Notification",
                table: "NotificationPlaceholders",
                column: "PlaceholderID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPreCondition_EmailTemplatePreConditionID",
                schema: "Notification",
                table: "NotificationPreCondition",
                column: "EmailTemplatePreConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPreCondition_NotificationID",
                schema: "Notification",
                table: "NotificationPreCondition",
                column: "NotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRecipientEmailAddress_ActivityNotificationID",
                schema: "Notification",
                table: "NotificationRecipientEmailAddress",
                column: "ActivityNotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRecipients_ActivityNotificationID",
                schema: "Notification",
                table: "NotificationRecipients",
                column: "ActivityNotificationID");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRecipients_PlaceholderID",
                schema: "Notification",
                table: "NotificationRecipients",
                column: "PlaceholderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailNotificationLogAddressee",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "EmailTemplateHeaderFooter",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationPlaceholders",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationPreCondition",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationRecipientEmailAddress",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationRecipients",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "EmailNotificationLog",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "NotificationStatus",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "EmailTemplatePreCondition",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "ActivityNotification",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "EmailTemplatePlaceholders",
                schema: "Notification");

            migrationBuilder.DropTable(
                name: "Notification",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "WorkflowNotificationType",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "EmailTemplatePlaceholderType",
                schema: "Master");
        }
    }
}

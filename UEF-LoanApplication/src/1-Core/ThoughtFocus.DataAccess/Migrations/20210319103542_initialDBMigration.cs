using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.DataAccess.Migrations
{
    public partial class initialDBMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Master");

            migrationBuilder.EnsureSchema(
                name: "Audit");

            migrationBuilder.EnsureSchema(
                name: "Contact");

            migrationBuilder.EnsureSchema(
                name: "User");

            migrationBuilder.CreateTable(
                name: "AccountStatus",
                schema: "Master",
                columns: table => new
                {
                    AccountStatusID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountStatusName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatus", x => x.AccountStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Action",
                schema: "Master",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.ActionID);
                });

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                schema: "Audit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessRole",
                schema: "Master",
                columns: table => new
                {
                    BusinessRoleID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessRoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessRole", x => x.BusinessRoleID);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                schema: "Master",
                columns: table => new
                {
                    CountryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryCallingCode = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Ethnicity",
                schema: "Master",
                columns: table => new
                {
                    EthnicityID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EthnicityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMinority = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnicity", x => x.EthnicityID);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                schema: "Master",
                columns: table => new
                {
                    GenderID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "Master",
                columns: table => new
                {
                    MenuID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuIconClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.MenuID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Master",
                columns: table => new
                {
                    RoleID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: false),
                    IsLoginRole = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Salutation",
                schema: "Master",
                columns: table => new
                {
                    SalutationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SalutationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salutation", x => x.SalutationID);
                });

            migrationBuilder.CreateTable(
                name: "SecurityQuestion",
                schema: "Master",
                columns: table => new
                {
                    SecurityQuestionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecurityQuestionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityQuestion", x => x.SecurityQuestionID);
                });

            migrationBuilder.CreateTable(
                name: "State",
                schema: "Master",
                columns: table => new
                {
                    StateID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateID);
                });

            migrationBuilder.CreateTable(
                name: "UrbanLeagueAffiliate",
                schema: "Master",
                columns: table => new
                {
                    AffiliateID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AffiliateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrbanLeagueAffiliate", x => x.AffiliateID);
                });

            migrationBuilder.CreateTable(
                name: "UserPasswordResetInfo",
                schema: "User",
                columns: table => new
                {
                    UserPasswordResetInfoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    PasswordResetDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TokenID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsExpiry = table.Column<bool>(type: "bit", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsAdminOrSelf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPasswordResetInfo", x => x.UserPasswordResetInfoID);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileLoginInfo",
                schema: "User",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsAccountActivated = table.Column<bool>(type: "bit", nullable: false),
                    IsLockedOut = table.Column<bool>(type: "bit", nullable: false),
                    LastPasswordChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstLoginDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogoutDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLockoutDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedLoginAttemptCount = table.Column<long>(type: "bigint", nullable: true),
                    FailedLoginAttemptDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedPasswordAttemptCount = table.Column<long>(type: "bigint", nullable: true),
                    FailedPasswordAttemptDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileLoginInfo", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "SubMenu",
                schema: "Master",
                columns: table => new
                {
                    SubMenuID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuID = table.Column<long>(type: "bigint", nullable: false),
                    SubMenuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubMenuDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubMenuURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true),
                    SubMenuIconClass = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubMenu", x => x.SubMenuID);
                    table.ForeignKey(
                        name: "FK_SubMenu_Menu_MenuID",
                        column: x => x.MenuID,
                        principalSchema: "Master",
                        principalTable: "Menu",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                schema: "Master",
                columns: table => new
                {
                    RolePermissionID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleID = table.Column<long>(type: "bigint", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => x.RolePermissionID);
                    table.ForeignKey(
                        name: "FK_RolePermission_Action_ActionID",
                        column: x => x.ActionID,
                        principalSchema: "Master",
                        principalTable: "Action",
                        principalColumn: "ActionID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Master",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "Contact",
                columns: table => new
                {
                    ContactID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalutationID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountStatusID = table.Column<long>(type: "bigint", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffiliateID = table.Column<long>(type: "bigint", nullable: false),
                    BusinessRoleID = table.Column<long>(type: "bigint", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contacts_AccountStatus_AccountStatusID",
                        column: x => x.AccountStatusID,
                        principalSchema: "Master",
                        principalTable: "AccountStatus",
                        principalColumn: "AccountStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_BusinessRole_BusinessRoleID",
                        column: x => x.BusinessRoleID,
                        principalSchema: "Master",
                        principalTable: "BusinessRole",
                        principalColumn: "BusinessRoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_Salutation_SalutationID",
                        column: x => x.SalutationID,
                        principalSchema: "Master",
                        principalTable: "Salutation",
                        principalColumn: "SalutationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contacts_UrbanLeagueAffiliate_AffiliateID",
                        column: x => x.AffiliateID,
                        principalSchema: "Master",
                        principalTable: "UrbanLeagueAffiliate",
                        principalColumn: "AffiliateID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RMRoleMenuSubMenu",
                schema: "Master",
                columns: table => new
                {
                    RoleMenuSubMenuID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    RoleID = table.Column<long>(type: "bigint", nullable: false),
                    MenuID = table.Column<long>(type: "bigint", nullable: false),
                    SubMenuID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RMRoleMenuSubMenu", x => x.RoleMenuSubMenuID);
                    table.ForeignKey(
                        name: "FK_RMRoleMenuSubMenu_Menu_MenuID",
                        column: x => x.MenuID,
                        principalSchema: "Master",
                        principalTable: "Menu",
                        principalColumn: "MenuID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RMRoleMenuSubMenu_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Master",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RMRoleMenuSubMenu_SubMenu_SubMenuID",
                        column: x => x.SubMenuID,
                        principalSchema: "Master",
                        principalTable: "SubMenu",
                        principalColumn: "SubMenuID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "User",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAccountActivated = table.Column<bool>(type: "bit", nullable: false),
                    IsLockedOut = table.Column<bool>(type: "bit", nullable: false),
                    LastPasswordChangedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstLoginDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLogoutDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLockoutDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedLoginAttemptCount = table.Column<long>(type: "bigint", nullable: true),
                    FailedLoginAttemptDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FailedPasswordAttemptCount = table.Column<long>(type: "bigint", nullable: true),
                    FailedPasswordAttemptDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountActivationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "Contact",
                        principalTable: "Contacts",
                        principalColumn: "ContactID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                schema: "User",
                columns: table => new
                {
                    UserRoleID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<long>(type: "bigint", nullable: false),
                    RoleID = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedByUserID = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.UserRoleID);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalSchema: "Master",
                        principalTable: "Role",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserID",
                        column: x => x.UserID,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AccountStatusID",
                schema: "Contact",
                table: "Contacts",
                column: "AccountStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_AffiliateID",
                schema: "Contact",
                table: "Contacts",
                column: "AffiliateID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_BusinessRoleID",
                schema: "Contact",
                table: "Contacts",
                column: "BusinessRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_SalutationID",
                schema: "Contact",
                table: "Contacts",
                column: "SalutationID");

            migrationBuilder.CreateIndex(
                name: "IX_RMRoleMenuSubMenu_MenuID",
                schema: "Master",
                table: "RMRoleMenuSubMenu",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_RMRoleMenuSubMenu_RoleID",
                schema: "Master",
                table: "RMRoleMenuSubMenu",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_RMRoleMenuSubMenu_SubMenuID",
                schema: "Master",
                table: "RMRoleMenuSubMenu",
                column: "SubMenuID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_ActionID",
                schema: "Master",
                table: "RolePermission",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_RoleID",
                schema: "Master",
                table: "RolePermission",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_SubMenu_MenuID",
                schema: "Master",
                table: "SubMenu",
                column: "MenuID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                schema: "User",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                schema: "User",
                table: "UserRole",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ContactID",
                schema: "User",
                table: "Users",
                column: "ContactID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrail",
                schema: "Audit");

            migrationBuilder.DropTable(
                name: "Country",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Ethnicity",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Gender",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "RMRoleMenuSubMenu",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "RolePermission",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "SecurityQuestion",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "State",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "UserPasswordResetInfo",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserProfileLoginInfo",
                schema: "User");

            migrationBuilder.DropTable(
                name: "UserRole",
                schema: "User");

            migrationBuilder.DropTable(
                name: "SubMenu",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Action",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "Contact");

            migrationBuilder.DropTable(
                name: "AccountStatus",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "BusinessRole",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "Salutation",
                schema: "Master");

            migrationBuilder.DropTable(
                name: "UrbanLeagueAffiliate",
                schema: "Master");
        }
    }
}

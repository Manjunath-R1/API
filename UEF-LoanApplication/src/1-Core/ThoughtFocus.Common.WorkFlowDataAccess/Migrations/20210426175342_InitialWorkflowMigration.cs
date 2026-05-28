using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThoughtFocus.Common.WorkFlowDataAccess.Migrations
{
    public partial class InitialWorkflowMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "WorkFlow");

            migrationBuilder.CreateTable(
                name: "ConditionType",
                schema: "WorkFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocalizeType",
                schema: "WorkFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParameterPurpose",
                schema: "WorkFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterPurpose", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RestrictionType",
                schema: "WorkFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransitionClassifier",
                schema: "WorkFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionClassifier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TriggerType",
                schema: "WorkFlow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ValidationType",
                schema: "WorkFlow",
                columns: table => new
                {
                    ValidationTypeID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidationTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationTypeDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationType", x => x.ValidationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignerModel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDefinition", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowProcessInstancePersistence",
                schema: "WorkFlow",
                columns: table => new
                {
                    WorkflowProcessInstancePersistenceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParameterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessInstanceID = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowDefinationID = table.Column<long>(type: "bigint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowProcessInstancePersistence", x => x.WorkflowProcessInstancePersistenceID);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowProcessInstanceStatus",
                schema: "WorkFlow",
                columns: table => new
                {
                    WorkflowProcessInstanceStatusID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lock = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProcessInstanceID = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowDefinationID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowProcessInstanceStatus", x => x.WorkflowProcessInstanceStatusID);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowProcessTimer",
                schema: "WorkFlow",
                columns: table => new
                {
                    WorkflowProcessTimerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextExecutionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessInstanceID = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowDefinationID = table.Column<long>(type: "bigint", nullable: false),
                    Ignore = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowProcessTimer", x => x.WorkflowProcessTimerID);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowProcessTransitionHistory",
                schema: "WorkFlow",
                columns: table => new
                {
                    WorkflowProcessTransitionHistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessInstanceID = table.Column<long>(type: "bigint", nullable: false),
                    WorkFlowDefinationID = table.Column<long>(type: "bigint", nullable: false),
                    ActorIdentityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutorIdentityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFinalised = table.Column<bool>(type: "bit", nullable: false),
                    FromActivityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromStateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToActivityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToStateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransitionClassifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransitionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TriggerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowProcessTransitionHistory", x => x.WorkflowProcessTransitionHistoryID);
                });

            migrationBuilder.CreateTable(
                name: "ValidationDefination",
                schema: "WorkFlow",
                columns: table => new
                {
                    ValidationDefinationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidationDefinationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationDefinationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ValidationDefinationErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationTypeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationDefination", x => x.ValidationDefinationID);
                    table.ForeignKey(
                        name: "FK_ValidationDefination_ValidationType_ValidationTypeID",
                        column: x => x.ValidationTypeID,
                        principalSchema: "WorkFlow",
                        principalTable: "ValidationType",
                        principalColumn: "ValidationTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeAsString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActionDefinition_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsInitial = table.Column<bool>(type: "bit", nullable: false),
                    IsFinal = table.Column<bool>(type: "bit", nullable: false),
                    IsForSetState = table.Column<bool>(type: "bit", nullable: false),
                    IsAutoSchemeUpdate = table.Column<bool>(type: "bit", nullable: false),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActivityDefinition_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorDefinitionExecuteRule",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorDefinitionExecuteRule", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActorDefinitionExecuteRule_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorDefinitionIsIdentity",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorDefinitionIsIdentity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActorDefinitionIsIdentity_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorDefinitionIsInRole",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorDefinitionIsInRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActorDefinitionIsInRole_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommandIconClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CommandDefinition_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LocalizeDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocalizeTypeID = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "bit", nullable: false),
                    ObjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Culture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizeDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LocalizeDefinition_LocalizeType_LocalizeTypeID",
                        column: x => x.LocalizeTypeID,
                        principalSchema: "WorkFlow",
                        principalTable: "LocalizeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LocalizeDefinition_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParameterDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeAsString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurposeID = table.Column<int>(type: "int", nullable: false),
                    SerializedDefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ParameterDefinition_ParameterPurpose_PurposeID",
                        column: x => x.PurposeID,
                        principalSchema: "WorkFlow",
                        principalTable: "ParameterPurpose",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParameterDefinition_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowProcessInstance",
                schema: "WorkFlow",
                columns: table => new
                {
                    WorkflowProcessInstanceID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessInstanceID = table.Column<long>(type: "bigint", nullable: false),
                    SchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    PreviousActivity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousActivityForDirect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousStateForDirect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousActivityForReverse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreviousStateForReverse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeterminingParametersChanged = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowProcessInstance", x => x.WorkflowProcessInstanceID);
                    table.ForeignKey(
                        name: "FK_WorkflowProcessInstance_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValidationFieldDefination",
                schema: "WorkFlow",
                columns: table => new
                {
                    ValidationFieldDefinationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidationFieldDefinationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationFieldValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValidationDefinationID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationFieldDefination", x => x.ValidationFieldDefinationID);
                    table.ForeignKey(
                        name: "FK_ValidationFieldDefination_ValidationDefination_ValidationDefinationID",
                        column: x => x.ValidationDefinationID,
                        principalSchema: "WorkFlow",
                        principalTable: "ValidationDefination",
                        principalColumn: "ValidationDefinationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConditionDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionTypeID = table.Column<int>(type: "int", nullable: false),
                    ResultOnPreExecution = table.Column<bool>(type: "bit", nullable: true),
                    Action_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ConditionDefinition_ActionDefinition_Action_ID",
                        column: x => x.Action_ID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActionDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConditionDefinition_ConditionType_ConditionTypeID",
                        column: x => x.ConditionTypeID,
                        principalSchema: "WorkFlow",
                        principalTable: "ConditionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActionDefinitionForActivity",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionDefinitionID = table.Column<long>(type: "bigint", nullable: true),
                    ActivityDefinitionID = table.Column<long>(type: "bigint", nullable: true),
                    IsPostExecution = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionDefinitionForActivity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActionDefinitionForActivity_ActionDefinition_ActionDefinitionID",
                        column: x => x.ActionDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActionDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActionDefinitionForActivity_ActivityDefinition_ActivityDefinitionID",
                        column: x => x.ActivityDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActivityDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TriggerDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommandID = table.Column<long>(type: "bigint", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TriggerDefinition_CommandDefinition_CommandID",
                        column: x => x.CommandID,
                        principalSchema: "WorkFlow",
                        principalTable: "CommandDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TriggerDefinition_TriggerType_TypeId",
                        column: x => x.TypeId,
                        principalSchema: "WorkFlow",
                        principalTable: "TriggerType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParameterDefinitionForAction",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsInputParameter = table.Column<bool>(type: "bit", nullable: false),
                    ParameterDefinitionID = table.Column<long>(type: "bigint", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ActionDefinitionID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParameterDefinitionForAction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ParameterDefinitionForAction_ActionDefinition_ActionDefinitionID",
                        column: x => x.ActionDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActionDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParameterDefinitionForAction_ParameterDefinition_ParameterDefinitionID",
                        column: x => x.ParameterDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "ParameterDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransitionDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionID = table.Column<long>(type: "bigint", nullable: false),
                    TransitionClassifierID = table.Column<int>(type: "int", nullable: false),
                    TriggerID = table.Column<long>(type: "bigint", nullable: false),
                    WorkflowDefinitionID = table.Column<long>(type: "bigint", nullable: false),
                    FromID = table.Column<long>(type: "bigint", nullable: true),
                    ToID = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TransitionDefinition_ActivityDefinition_FromID",
                        column: x => x.FromID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActivityDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransitionDefinition_ActivityDefinition_ToID",
                        column: x => x.ToID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActivityDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransitionDefinition_ConditionDefinition_ConditionID",
                        column: x => x.ConditionID,
                        principalSchema: "WorkFlow",
                        principalTable: "ConditionDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionDefinition_TransitionClassifier_TransitionClassifierID",
                        column: x => x.TransitionClassifierID,
                        principalSchema: "WorkFlow",
                        principalTable: "TransitionClassifier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionDefinition_TriggerDefinition_TriggerID",
                        column: x => x.TriggerID,
                        principalSchema: "WorkFlow",
                        principalTable: "TriggerDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionDefinition_WorkflowDefinition_WorkflowDefinitionID",
                        column: x => x.WorkflowDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "WorkflowDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestrictionDefinition",
                schema: "WorkFlow",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestrictionType_Id = table.Column<int>(type: "int", nullable: true),
                    Transition_ID = table.Column<long>(type: "bigint", nullable: true),
                    ActorDefinitionExecuteRule_ID = table.Column<long>(type: "bigint", nullable: true),
                    ActorDefinitionIsIdentity_ID = table.Column<long>(type: "bigint", nullable: true),
                    ActorDefinitionIsInRole_ID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestrictionDefinition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RestrictionDefinition_ActorDefinitionExecuteRule_ActorDefinitionExecuteRule_ID",
                        column: x => x.ActorDefinitionExecuteRule_ID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActorDefinitionExecuteRule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RestrictionDefinition_ActorDefinitionIsIdentity_ActorDefinitionIsIdentity_ID",
                        column: x => x.ActorDefinitionIsIdentity_ID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActorDefinitionIsIdentity",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RestrictionDefinition_ActorDefinitionIsInRole_ActorDefinitionIsInRole_ID",
                        column: x => x.ActorDefinitionIsInRole_ID,
                        principalSchema: "WorkFlow",
                        principalTable: "ActorDefinitionIsInRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RestrictionDefinition_RestrictionType_RestrictionType_Id",
                        column: x => x.RestrictionType_Id,
                        principalSchema: "WorkFlow",
                        principalTable: "RestrictionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RestrictionDefinition_TransitionDefinition_Transition_ID",
                        column: x => x.Transition_ID,
                        principalSchema: "WorkFlow",
                        principalTable: "TransitionDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransitionValidationDefination",
                schema: "WorkFlow",
                columns: table => new
                {
                    TransitionValidationDefinationID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransitionValidationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEnabled = table.Column<bool>(type: "bit", nullable: false),
                    ValidationDefinationID = table.Column<long>(type: "bigint", nullable: false),
                    TransitionDefinitionID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransitionValidationDefination", x => x.TransitionValidationDefinationID);
                    table.ForeignKey(
                        name: "FK_TransitionValidationDefination_TransitionDefinition_TransitionDefinitionID",
                        column: x => x.TransitionDefinitionID,
                        principalSchema: "WorkFlow",
                        principalTable: "TransitionDefinition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransitionValidationDefination_ValidationDefination_ValidationDefinationID",
                        column: x => x.ValidationDefinationID,
                        principalSchema: "WorkFlow",
                        principalTable: "ValidationDefination",
                        principalColumn: "ValidationDefinationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionDefinition_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "ActionDefinition",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionDefinitionForActivity_ActionDefinitionID",
                schema: "WorkFlow",
                table: "ActionDefinitionForActivity",
                column: "ActionDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionDefinitionForActivity_ActivityDefinitionID",
                schema: "WorkFlow",
                table: "ActionDefinitionForActivity",
                column: "ActivityDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDefinition_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "ActivityDefinition",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorDefinitionExecuteRule_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "ActorDefinitionExecuteRule",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorDefinitionIsIdentity_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "ActorDefinitionIsIdentity",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ActorDefinitionIsInRole_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "ActorDefinitionIsInRole",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_CommandDefinition_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "CommandDefinition",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionDefinition_Action_ID",
                schema: "WorkFlow",
                table: "ConditionDefinition",
                column: "Action_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionDefinition_ConditionTypeID",
                schema: "WorkFlow",
                table: "ConditionDefinition",
                column: "ConditionTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizeDefinition_LocalizeTypeID",
                schema: "WorkFlow",
                table: "LocalizeDefinition",
                column: "LocalizeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizeDefinition_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "LocalizeDefinition",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterDefinition_PurposeID",
                schema: "WorkFlow",
                table: "ParameterDefinition",
                column: "PurposeID");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterDefinition_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "ParameterDefinition",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterDefinitionForAction_ActionDefinitionID",
                schema: "WorkFlow",
                table: "ParameterDefinitionForAction",
                column: "ActionDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ParameterDefinitionForAction_ParameterDefinitionID",
                schema: "WorkFlow",
                table: "ParameterDefinitionForAction",
                column: "ParameterDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictionDefinition_ActorDefinitionExecuteRule_ID",
                schema: "WorkFlow",
                table: "RestrictionDefinition",
                column: "ActorDefinitionExecuteRule_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictionDefinition_ActorDefinitionIsIdentity_ID",
                schema: "WorkFlow",
                table: "RestrictionDefinition",
                column: "ActorDefinitionIsIdentity_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictionDefinition_ActorDefinitionIsInRole_ID",
                schema: "WorkFlow",
                table: "RestrictionDefinition",
                column: "ActorDefinitionIsInRole_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictionDefinition_RestrictionType_Id",
                schema: "WorkFlow",
                table: "RestrictionDefinition",
                column: "RestrictionType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_RestrictionDefinition_Transition_ID",
                schema: "WorkFlow",
                table: "RestrictionDefinition",
                column: "Transition_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionDefinition_ConditionID",
                schema: "WorkFlow",
                table: "TransitionDefinition",
                column: "ConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionDefinition_FromID",
                schema: "WorkFlow",
                table: "TransitionDefinition",
                column: "FromID");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionDefinition_ToID",
                schema: "WorkFlow",
                table: "TransitionDefinition",
                column: "ToID");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionDefinition_TransitionClassifierID",
                schema: "WorkFlow",
                table: "TransitionDefinition",
                column: "TransitionClassifierID");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionDefinition_TriggerID",
                schema: "WorkFlow",
                table: "TransitionDefinition",
                column: "TriggerID");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionDefinition_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "TransitionDefinition",
                column: "WorkflowDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionValidationDefination_TransitionDefinitionID",
                schema: "WorkFlow",
                table: "TransitionValidationDefination",
                column: "TransitionDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_TransitionValidationDefination_ValidationDefinationID",
                schema: "WorkFlow",
                table: "TransitionValidationDefination",
                column: "ValidationDefinationID");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerDefinition_CommandID",
                schema: "WorkFlow",
                table: "TriggerDefinition",
                column: "CommandID");

            migrationBuilder.CreateIndex(
                name: "IX_TriggerDefinition_TypeId",
                schema: "WorkFlow",
                table: "TriggerDefinition",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidationDefination_ValidationTypeID",
                schema: "WorkFlow",
                table: "ValidationDefination",
                column: "ValidationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ValidationFieldDefination_ValidationDefinationID",
                schema: "WorkFlow",
                table: "ValidationFieldDefination",
                column: "ValidationDefinationID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkflowProcessInstance_WorkflowDefinitionID",
                schema: "WorkFlow",
                table: "WorkflowProcessInstance",
                column: "WorkflowDefinitionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionDefinitionForActivity",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "LocalizeDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ParameterDefinitionForAction",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "RestrictionDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "TransitionValidationDefination",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ValidationFieldDefination",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "WorkflowProcessInstance",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "WorkflowProcessInstancePersistence",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "WorkflowProcessInstanceStatus",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "WorkflowProcessTimer",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "WorkflowProcessTransitionHistory",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "LocalizeType",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ParameterDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ActorDefinitionExecuteRule",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ActorDefinitionIsIdentity",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ActorDefinitionIsInRole",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "RestrictionType",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "TransitionDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ValidationDefination",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ParameterPurpose",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ActivityDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ConditionDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "TransitionClassifier",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "TriggerDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ValidationType",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ActionDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "ConditionType",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "CommandDefinition",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "TriggerType",
                schema: "WorkFlow");

            migrationBuilder.DropTable(
                name: "WorkflowDefinition",
                schema: "WorkFlow");
        }
    }
}

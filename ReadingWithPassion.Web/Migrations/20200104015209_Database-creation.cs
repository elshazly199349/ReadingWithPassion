using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ReadingWithPassion.Web.Migrations
{
    public partial class Databasecreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Desciprion = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    DurationInSeconds = table.Column<int>(nullable: true),
                    InsertionDateTime = table.Column<DateTime>(nullable: false),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    AdminId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkShops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InsetionDateTime = table.Column<DateTime>(nullable: true),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    Duration = table.Column<int>(nullable: true),
                    AdminId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShops_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Desciption = table.Column<string>(nullable: true),
                    InsertionDateTime = table.Column<DateTime>(nullable: false),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    DurationInSeconds = table.Column<int>(nullable: true),
                    GameId = table.Column<int>(nullable: false),
                    AdminId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Questions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkShop_Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertionDateTime = table.Column<DateTime>(nullable: false),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    AdminId = table.Column<string>(nullable: true),
                    GameId = table.Column<int>(nullable: false),
                    WorkShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShop_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShop_Games_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkShop_Games_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkShop_Games_WorkShops_WorkShopId",
                        column: x => x.WorkShopId,
                        principalTable: "WorkShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkShop_Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    DurationInMinutes = table.Column<int>(nullable: false),
                    AvailableSpaces = table.Column<int>(nullable: false),
                    InsertionDateTime = table.Column<DateTime>(nullable: false),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    AdminId = table.Column<string>(nullable: true),
                    WorkShopId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShop_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkShop_Schedules_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkShop_Schedules_WorkShops_WorkShopId",
                        column: x => x.WorkShopId,
                        principalTable: "WorkShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question_Points",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    DurtationInSeconds = table.Column<int>(nullable: false),
                    InsertionDateTime = table.Column<DateTime>(nullable: false),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    AdminId = table.Column<string>(nullable: true),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Points_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Points_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_WorkShop_Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertionDateTime = table.Column<DateTime>(nullable: false),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsPaid = table.Column<bool>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    AdminId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    WorkShop_Schedule_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_WorkShop_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_WorkShop_Schedules_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_WorkShop_Schedules_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_WorkShop_Schedules_WorkShop_Schedules_WorkShop_Schedule_Id",
                        column: x => x.WorkShop_Schedule_Id,
                        principalTable: "WorkShop_Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question_Point_Choices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    IsCorrectAnswer = table.Column<bool>(nullable: false),
                    InsertionDateTime = table.Column<DateTime>(nullable: false),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    AdminId = table.Column<string>(nullable: true),
                    Question_Point_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question_Point_Choices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Question_Point_Choices_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Question_Point_Choices_Question_Points_Question_Point_Id",
                        column: x => x.Question_Point_Id,
                        principalTable: "Question_Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_Games",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsertionDateTime = table.Column<DateTime>(nullable: false),
                    ModificationDateTime = table.Column<DateTime>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    durationInSeconds = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    User_WorkShop_Schedule_Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Games_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Games_User_WorkShop_Schedules_User_WorkShop_Schedule_Id",
                        column: x => x.User_WorkShop_Schedule_Id,
                        principalTable: "User_WorkShop_Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_AdminId",
                table: "Games",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Point_Choices_AdminId",
                table: "Question_Point_Choices",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Point_Choices_Question_Point_Id",
                table: "Question_Point_Choices",
                column: "Question_Point_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Points_AdminId",
                table: "Question_Points",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_Points_QuestionId",
                table: "Question_Points",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_AdminId",
                table: "Questions",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_GameId",
                table: "Questions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Games_GameId",
                table: "User_Games",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Games_User_WorkShop_Schedule_Id",
                table: "User_Games",
                column: "User_WorkShop_Schedule_Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_WorkShop_Schedules_AdminId",
                table: "User_WorkShop_Schedules",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WorkShop_Schedules_UserId",
                table: "User_WorkShop_Schedules",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WorkShop_Schedules_WorkShop_Schedule_Id",
                table: "User_WorkShop_Schedules",
                column: "WorkShop_Schedule_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShop_Games_AdminId",
                table: "WorkShop_Games",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShop_Games_GameId",
                table: "WorkShop_Games",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShop_Games_WorkShopId",
                table: "WorkShop_Games",
                column: "WorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShop_Schedules_AdminId",
                table: "WorkShop_Schedules",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShop_Schedules_WorkShopId",
                table: "WorkShop_Schedules",
                column: "WorkShopId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkShops_AdminId",
                table: "WorkShops",
                column: "AdminId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question_Point_Choices");

            migrationBuilder.DropTable(
                name: "User_Games");

            migrationBuilder.DropTable(
                name: "WorkShop_Games");

            migrationBuilder.DropTable(
                name: "Question_Points");

            migrationBuilder.DropTable(
                name: "User_WorkShop_Schedules");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "WorkShop_Schedules");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "WorkShops");
        }
    }
}

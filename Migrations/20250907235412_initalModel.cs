using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace csweb.Migrations
{
    /// <inheritdoc />
    public partial class initalModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ReadingLists",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReadingLists_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoryReadingList",
                columns: table => new
                {
                    HistoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadingListsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryReadingList", x => new { x.HistoriesId, x.ReadingListsID });
                    table.ForeignKey(
                        name: "FK_HistoryReadingList_Histories_HistoriesId",
                        column: x => x.HistoriesId,
                        principalTable: "Histories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryReadingList_ReadingLists_ReadingListsID",
                        column: x => x.ReadingListsID,
                        principalTable: "ReadingLists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReadingListHistorys",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReadingListID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HistoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadingListHistorys", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReadingListHistorys_Histories_HistoryID",
                        column: x => x.HistoryID,
                        principalTable: "Histories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReadingListHistorys_ReadingLists_ReadingListID",
                        column: x => x.ReadingListID,
                        principalTable: "ReadingLists",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ReadingListHistorys_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Histories_UserID",
                table: "Histories",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryReadingList_ReadingListsID",
                table: "HistoryReadingList",
                column: "ReadingListsID");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingListHistorys_HistoryID",
                table: "ReadingListHistorys",
                column: "HistoryID");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingListHistorys_ReadingListID",
                table: "ReadingListHistorys",
                column: "ReadingListID");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingListHistorys_UserID",
                table: "ReadingListHistorys",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ReadingLists_UserID",
                table: "ReadingLists",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryReadingList");

            migrationBuilder.DropTable(
                name: "ReadingListHistorys");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "ReadingLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

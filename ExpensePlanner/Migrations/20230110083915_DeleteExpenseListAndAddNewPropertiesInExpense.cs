using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensePlanner.Migrations
{
    public partial class DeleteExpenseListAndAddNewPropertiesInExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseLists_ExpenseListId",
                table: "Expenses");

            migrationBuilder.DropTable(
                name: "ExpenseDto");

            migrationBuilder.DropTable(
                name: "ExpenseLists");

            migrationBuilder.RenameColumn(
                name: "ExpenseListId",
                table: "Expenses",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_ExpenseListId",
                table: "Expenses",
                newName: "IX_Expenses_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Expenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RealizedDate",
                table: "Expenses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_UserId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "RealizedDate",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Expenses",
                newName: "ExpenseListId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                newName: "IX_Expenses_ExpenseListId");

            migrationBuilder.CreateTable(
                name: "ExpenseDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpenseType = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseLists_UserId",
                table: "ExpenseLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseLists_ExpenseListId",
                table: "Expenses",
                column: "ExpenseListId",
                principalTable: "ExpenseLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicTacToe.App.Migrations
{
    public partial class updateToPlayerEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsX",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool?[,]>(
                name: "GameBoard",
                table: "Games",
                type: "boolean[]",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsX",
                table: "Players");

            migrationBuilder.AlterColumn<string[]>(
                name: "GameBoard",
                table: "Games",
                type: "text[]",
                nullable: false,
                oldClrType: typeof(bool?[,]),
                oldType: "boolean[]");
        }
    }
}

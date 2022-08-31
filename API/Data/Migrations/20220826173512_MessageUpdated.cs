using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    public partial class MessageUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_RecipientId1",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecipientId1",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RecipientId1",
                table: "Messages");

            migrationBuilder.AlterColumn<bool>(
                name: "SenderDeleted",
                table: "Messages",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AlterColumn<Guid>(
                name: "RecipientId",
                table: "Messages",
                type: "char(36)",
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<bool>(
                name: "RecipientDeleted",
                table: "Messages",
                type: "tinyint(1)",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.AddColumn<DateTime>(
                name: "MessageSent",
                table: "Messages",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_RecipientId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageSent",
                table: "Messages");

            migrationBuilder.AlterColumn<bool>(
                name: "SenderDeleted",
                table: "Messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RecipientId",
                table: "Messages",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AlterColumn<bool>(
                name: "RecipientDeleted",
                table: "Messages",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RecipientId1",
                table: "Messages",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_RecipientId1",
                table: "Messages",
                column: "RecipientId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_RecipientId1",
                table: "Messages",
                column: "RecipientId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

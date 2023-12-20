using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixCandidateRelation4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Replies_Id",
                table: "Statuses");

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId",
                table: "Replies",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Replies_StatusId",
                table: "Replies",
                column: "StatusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Statuses_StatusId",
                table: "Replies",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Statuses_StatusId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_StatusId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Replies");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Replies_Id",
                table: "Statuses",
                column: "Id",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

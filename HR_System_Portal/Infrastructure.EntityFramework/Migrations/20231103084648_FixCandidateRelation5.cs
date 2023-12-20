using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FixCandidateRelation5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Statuses_StatusId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_StatusId",
                table: "Replies");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_JobReplyId",
                table: "Statuses",
                column: "JobReplyId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Replies_JobReplyId",
                table: "Statuses",
                column: "JobReplyId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Replies_JobReplyId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_JobReplyId",
                table: "Statuses");

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
    }
}

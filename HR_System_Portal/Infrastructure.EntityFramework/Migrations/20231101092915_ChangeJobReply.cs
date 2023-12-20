using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ChangeJobReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobReply_Candidates_CandidateId",
                table: "JobReply");

            migrationBuilder.DropForeignKey(
                name: "FK_JobReply_Jobs_JobId",
                table: "JobReply");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_JobReply_JobReplyId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_JobReplyId",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobReply",
                table: "JobReply");

            migrationBuilder.DropColumn(
                name: "JobReplyId",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "JobReply",
                newName: "Replies");

            migrationBuilder.RenameIndex(
                name: "IX_JobReply_JobId",
                table: "Replies",
                newName: "IX_Replies_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_JobReply_CandidateId",
                table: "Replies",
                newName: "IX_Replies_CandidateId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Statuses",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsActual",
                table: "Statuses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Replies",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Replies",
                table: "Replies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Candidates_CandidateId",
                table: "Replies",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Jobs_JobId",
                table: "Replies",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Replies_Id",
                table: "Statuses",
                column: "Id",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Candidates_CandidateId",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Jobs_JobId",
                table: "Replies");

            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Replies_Id",
                table: "Statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Replies",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "IsActual",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Replies");

            migrationBuilder.RenameTable(
                name: "Replies",
                newName: "JobReply");

            migrationBuilder.RenameIndex(
                name: "IX_Replies_JobId",
                table: "JobReply",
                newName: "IX_JobReply_JobId");

            migrationBuilder.RenameIndex(
                name: "IX_Replies_CandidateId",
                table: "JobReply",
                newName: "IX_JobReply_CandidateId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Statuses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<Guid>(
                name: "JobReplyId",
                table: "Statuses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobReply",
                table: "JobReply",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_JobReplyId",
                table: "Statuses",
                column: "JobReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobReply_Candidates_CandidateId",
                table: "JobReply",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobReply_Jobs_JobId",
                table: "JobReply",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_JobReply_JobReplyId",
                table: "Statuses",
                column: "JobReplyId",
                principalTable: "JobReply",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

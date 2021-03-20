using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MailSender.Migrations
{
    public partial class addScheduler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchedulerId",
                table: "Recipients",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Scheduler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTimeSend = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerId = table.Column<int>(type: "int", nullable: true),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    MessageId = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scheduler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scheduler_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scheduler_Senders_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Senders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Scheduler_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_SchedulerId",
                table: "Recipients",
                column: "SchedulerId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduler_MessageId",
                table: "Scheduler",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduler_SenderId",
                table: "Scheduler",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Scheduler_ServerId",
                table: "Scheduler",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipients_Scheduler_SchedulerId",
                table: "Recipients",
                column: "SchedulerId",
                principalTable: "Scheduler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipients_Scheduler_SchedulerId",
                table: "Recipients");

            migrationBuilder.DropTable(
                name: "Scheduler");

            migrationBuilder.DropIndex(
                name: "IX_Recipients_SchedulerId",
                table: "Recipients");

            migrationBuilder.DropColumn(
                name: "SchedulerId",
                table: "Recipients");
        }
    }
}

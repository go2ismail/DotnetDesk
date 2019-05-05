using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace src.Migrations
{
    public partial class _20190405113150_additionOfTicketThread : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TicketThread",
                columns: table => new
                {
                    ticketThreadId = table.Column<Guid>(nullable: false),
                    Comment = table.Column<string>(maxLength: 250, nullable: false),
                    ticketId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketThread", x => x.ticketThreadId);
                    table.ForeignKey(
                        name: "FK_TicketThread_Ticket_ticketId",
                        column: x => x.ticketId,
                        principalTable: "Ticket",
                        principalColumn: "ticketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketThread_ticketId",
                table: "TicketThread",
                column: "ticketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketThread");
        }
    }
}

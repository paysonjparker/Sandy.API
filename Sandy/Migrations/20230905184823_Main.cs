using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sandy.Migrations
{
    /// <inheritdoc />
    public partial class Main : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_GolferId",
                table: "Scores",
                column: "GolferId");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Golfers_GolferId",
                table: "Scores",
                column: "GolferId",
                principalTable: "Golfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Golfers_GolferId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_GolferId",
                table: "Scores");

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Differential = table.Column<float>(type: "real", nullable: false),
                    GolferId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Score_Golfers_GolferId",
                        column: x => x.GolferId,
                        principalTable: "Golfers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Score_GolferId",
                table: "Scores",
                column: "GolferId");
        }
    }
}

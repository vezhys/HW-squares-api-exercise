using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace squares_api_excercise.Migrations
{
    /// <inheritdoc />
    public partial class RedesignSquareModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PointSquare");

            migrationBuilder.AddColumn<int>(
                name: "P1Id",
                table: "Squares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "P2Id",
                table: "Squares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "P3Id",
                table: "Squares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "P4Id",
                table: "Squares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Squares_P1Id",
                table: "Squares",
                column: "P1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Squares_P2Id",
                table: "Squares",
                column: "P2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Squares_P3Id",
                table: "Squares",
                column: "P3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Squares_P4Id",
                table: "Squares",
                column: "P4Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Squares_Points_P1Id",
                table: "Squares",
                column: "P1Id",
                principalTable: "Points",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Squares_Points_P2Id",
                table: "Squares",
                column: "P2Id",
                principalTable: "Points",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Squares_Points_P3Id",
                table: "Squares",
                column: "P3Id",
                principalTable: "Points",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Squares_Points_P4Id",
                table: "Squares",
                column: "P4Id",
                principalTable: "Points",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Squares_Points_P1Id",
                table: "Squares");

            migrationBuilder.DropForeignKey(
                name: "FK_Squares_Points_P2Id",
                table: "Squares");

            migrationBuilder.DropForeignKey(
                name: "FK_Squares_Points_P3Id",
                table: "Squares");

            migrationBuilder.DropForeignKey(
                name: "FK_Squares_Points_P4Id",
                table: "Squares");

            migrationBuilder.DropIndex(
                name: "IX_Squares_P1Id",
                table: "Squares");

            migrationBuilder.DropIndex(
                name: "IX_Squares_P2Id",
                table: "Squares");

            migrationBuilder.DropIndex(
                name: "IX_Squares_P3Id",
                table: "Squares");

            migrationBuilder.DropIndex(
                name: "IX_Squares_P4Id",
                table: "Squares");

            migrationBuilder.DropColumn(
                name: "P1Id",
                table: "Squares");

            migrationBuilder.DropColumn(
                name: "P2Id",
                table: "Squares");

            migrationBuilder.DropColumn(
                name: "P3Id",
                table: "Squares");

            migrationBuilder.DropColumn(
                name: "P4Id",
                table: "Squares");

            migrationBuilder.CreateTable(
                name: "PointSquare",
                columns: table => new
                {
                    PointsId = table.Column<int>(type: "int", nullable: false),
                    SquareId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PointSquare", x => new { x.PointsId, x.SquareId });
                    table.ForeignKey(
                        name: "FK_PointSquare_Points_PointsId",
                        column: x => x.PointsId,
                        principalTable: "Points",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PointSquare_Squares_SquareId",
                        column: x => x.SquareId,
                        principalTable: "Squares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PointSquare_SquareId",
                table: "PointSquare",
                column: "SquareId");
        }
    }
}

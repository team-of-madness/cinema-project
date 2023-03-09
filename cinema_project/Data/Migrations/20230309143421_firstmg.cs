using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinema_project.Data.Migrations
{
    public partial class firstmg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "User_id",
                table: "Tickets",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Id_session",
                table: "Tickets",
                newName: "SessionId");

            migrationBuilder.RenameColumn(
                name: "Start_date",
                table: "Sessions",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Id_movie",
                table: "Sessions",
                newName: "MovieId");

            migrationBuilder.RenameColumn(
                name: "End_date",
                table: "Sessions",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "Min_age",
                table: "Movies",
                newName: "MinAge");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Producer",
                table: "Movies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Producer",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Tickets",
                newName: "User_id");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Tickets",
                newName: "Id_session");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Sessions",
                newName: "Start_date");

            migrationBuilder.RenameColumn(
                name: "MovieId",
                table: "Sessions",
                newName: "Id_movie");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Sessions",
                newName: "End_date");

            migrationBuilder.RenameColumn(
                name: "MinAge",
                table: "Movies",
                newName: "Min_age");
        }
    }
}

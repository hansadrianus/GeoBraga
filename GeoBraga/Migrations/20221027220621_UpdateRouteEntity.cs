using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoBraga.Migrations
{
    public partial class UpdateRouteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Route");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Route");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Route",
                newName: "LineNodes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Route",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Route");

            migrationBuilder.RenameColumn(
                name: "LineNodes",
                table: "Route",
                newName: "Location");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Route",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Route",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

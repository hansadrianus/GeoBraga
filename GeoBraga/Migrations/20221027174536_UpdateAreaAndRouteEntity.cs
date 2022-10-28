using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GeoBraga.Migrations
{
    public partial class UpdateAreaAndRouteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Area");

            migrationBuilder.DropColumn(
                name: "Sequence",
                table: "Area");

            migrationBuilder.RenameColumn(
                name: "Node",
                table: "Area",
                newName: "PolygonNodes");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Area",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Area");

            migrationBuilder.RenameColumn(
                name: "PolygonNodes",
                table: "Area",
                newName: "Node");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Area",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sequence",
                table: "Area",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

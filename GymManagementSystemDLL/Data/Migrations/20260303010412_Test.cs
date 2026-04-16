using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementSystemDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartmentName",
                table: "sessoins",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "health",
                table: "Members",
                newName: "Wight");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "sessoins",
                newName: "DepartmentName");

            migrationBuilder.RenameColumn(
                name: "Wight",
                table: "Members",
                newName: "health");
        }
    }
}

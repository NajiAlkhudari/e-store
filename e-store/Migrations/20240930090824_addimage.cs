using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_store.Migrations
{
    public partial class addimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customers",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "CustomerDto",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "CartDto",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "ProductDto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CustomerDto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "CartDto",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "ProductDto");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CustomerDto");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "CartDto");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "CustomerDto",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "CartDto",
                newName: "FullName");
        }
    }
}

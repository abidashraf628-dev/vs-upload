using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Migrations
{
    /// <inheritdoc />
    public partial class STATUSCOLUMN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "EmployeeName",
            //    table: "AssignTasks");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "AssignTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AssignTasks");

            //migrationBuilder.AddColumn<string>(
            //    name: "EmployeeName",
            //    table: "AssignTasks",
            //    type: "nvarchar(150)",
            //    maxLength: 150,
            //    nullable: false,
            //    defaultValue: "");
        }
    }
}

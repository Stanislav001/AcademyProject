using Microsoft.EntityFrameworkCore.Migrations;

namespace Date.Migrations
{
    public partial class Update_User_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "653b5c37-db40-466f-bb2c-a4729208aef8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fdb7f7d6-202f-4048-8f42-9bb26cf1d4a1");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "39139e09-3ce1-4eca-89ac-46ed9dffaa98");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "d610d074-8359-478e-9c69-8b096705a3b0");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "077a03b7-4f6e-4e5f-a221-b5f8eee68b94");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3f53db88-cde0-4218-9714-43a92fb83ee2", "43577666-0b10-4e02-9506-3504806353de", "Admin", null },
                    { "69c48557-9fe8-483a-8e4b-b3ec6a3560bd", "b9176625-5a00-4095-a38e-f3d00615a315", "User", null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseName", "Description", "Duration", "ImageName", "ManagerId", "Price", "UserId" },
                values: new object[] { "7bf739d6-7710-41dd-a208-d79dff5ba776", "JavaScript", "", "6", null, null, 800m, null });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "City", "CoursesNumber", "Email", "FirstName", "ImageName", "LastName", "ManagerId", "PhoneNumber", "SecondName", "StudentNumber", "Year" },
                values: new object[] { "54fc5e64-d22f-4c81-b8c0-01f1f918d9d8", "Sofia", 0, "petrov@gmail.com", "Ivan", null, "Petrov", null, "302-444-1234", "Hristov", null, 19 });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Education", "Email", "Experience", "FirstName", "ImageName", "LastName", "ManagerId", "PhoneNumber", "Position", "Salary", "SecondName", "Year" },
                values: new object[] { "e435d9dd-3ae2-401e-924d-02e64cac70cc", "Higher", "georgiev@gmail.com", 6, "Petar", null, "Georgiev", null, "202-555-0108", "Teacher", 2000m, "Petrov", 21 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f53db88-cde0-4218-9714-43a92fb83ee2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "69c48557-9fe8-483a-8e4b-b3ec6a3560bd");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "7bf739d6-7710-41dd-a208-d79dff5ba776");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "54fc5e64-d22f-4c81-b8c0-01f1f918d9d8");

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: "e435d9dd-3ae2-401e-924d-02e64cac70cc");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "fdb7f7d6-202f-4048-8f42-9bb26cf1d4a1", "4d3e39f0-cf7a-4d4a-acdd-336eac086628", "Admin", null },
                    { "653b5c37-db40-466f-bb2c-a4729208aef8", "e67855f4-884d-4f90-88ac-bbf9da55fcf4", "User", null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseName", "Description", "Duration", "ImageName", "ManagerId", "Price", "UserId" },
                values: new object[] { "39139e09-3ce1-4eca-89ac-46ed9dffaa98", "JavaScript", "", "6", null, null, 800m, null });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "City", "CoursesNumber", "Email", "FirstName", "ImageName", "LastName", "ManagerId", "PhoneNumber", "SecondName", "StudentNumber", "Year" },
                values: new object[] { "d610d074-8359-478e-9c69-8b096705a3b0", "Sofia", 0, "petrov@gmail.com", "Ivan", null, "Petrov", null, "302-444-1234", "Hristov", null, 19 });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Education", "Email", "Experience", "FirstName", "ImageName", "LastName", "ManagerId", "PhoneNumber", "Position", "Salary", "SecondName", "Year" },
                values: new object[] { "077a03b7-4f6e-4e5f-a221-b5f8eee68b94", "Higher", "georgiev@gmail.com", 6, "Petar", null, "Georgiev", null, "202-555-0108", "Teacher", 2000m, "Petrov", 21 });
        }
    }
}

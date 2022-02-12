using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Date.Migrations
{
    public partial class add_new_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9d4befa-d5ab-40af-b892-2795b999e06f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf7ea7b9-db91-439e-904a-93d9291737ec");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "dff5807b-21e0-4c01-a814-89c9bffb798b");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "531b4da0-d5a5-4fd0-971f-9380fe60731f");

            migrationBuilder.CreateTable(
                name: "SaveCourseUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourseId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaveCourseUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaveCourseUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaveCourseUsers_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "43475417-be0e-4773-8e6a-7f4410375b6c", "dcd41b64-8880-4d70-897e-3d1157e05e2d", "Admin", null },
                    { "d4f9d61d-8149-4d60-805b-f59e18b96164", "54c26a70-a0fe-4b73-93aa-44fd8e94ccad", "User", null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseId", "CourseName", "Description", "Duration", "EndDate", "ImageName", "Price", "StartDate", "StudentId", "UserId", "Votes", "isCompleted", "isStarted" },
                values: new object[] { "07764004-80da-4a97-ab88-ab681671d900", null, "JavaScript", "", "6", null, null, 800m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0, false, false });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "City", "CourseId", "CourseName", "CoursesNumber", "Email", "FirstName", "ImageName", "LastName", "PhoneNumber", "SecondName", "StudentNumber", "Year" },
                values: new object[] { "5a262537-4aa6-48a7-b0bf-aabd5777f956", "Sofia", null, null, 0, "petrov@gmail.com", "Ivan", null, "Petrov", "302-444-1234", "Hristov", null, 19 });

            migrationBuilder.CreateIndex(
                name: "IX_SaveCourseUsers_CourseId",
                table: "SaveCourseUsers",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SaveCourseUsers_UserId",
                table: "SaveCourseUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaveCourseUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43475417-be0e-4773-8e6a-7f4410375b6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4f9d61d-8149-4d60-805b-f59e18b96164");

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: "07764004-80da-4a97-ab88-ab681671d900");

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: "5a262537-4aa6-48a7-b0bf-aabd5777f956");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9d4befa-d5ab-40af-b892-2795b999e06f", "5dafbfc3-cf5a-41da-b719-a89b9385b3b3", "Admin", null },
                    { "bf7ea7b9-db91-439e-904a-93d9291737ec", "3a2cff9c-65c7-4386-bdfd-89c620da91ba", "User", null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CourseId", "CourseName", "Description", "Duration", "EndDate", "ImageName", "Price", "StartDate", "StudentId", "UserId", "Votes", "isCompleted", "isStarted" },
                values: new object[] { "dff5807b-21e0-4c01-a814-89c9bffb798b", null, "JavaScript", "", "6", null, null, 800m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 0, false, false });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "City", "CourseId", "CourseName", "CoursesNumber", "Email", "FirstName", "ImageName", "LastName", "PhoneNumber", "SecondName", "StudentNumber", "Year" },
                values: new object[] { "531b4da0-d5a5-4fd0-971f-9380fe60731f", "Sofia", null, null, 0, "petrov@gmail.com", "Ivan", null, "Petrov", "302-444-1234", "Hristov", null, 19 });
        }
    }
}

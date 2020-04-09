using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class ChangeTitelToOnderwerp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e23ae18-a2a8-4609-acbe-3e387ea671bf");

            migrationBuilder.DropColumn(
                name: "Titel",
                table: "Quizzen");

            migrationBuilder.DropColumn(
                name: "Paswoord",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Onderwerp",
                table: "Quizzen",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Naam" },
                values: new object[] { "5aa79098-9e74-492d-ad90-afc03cd9f81c", 0, "52d26da1-b742-4815-b13f-0b93257f8666", "Gebruiker", "Judith.van.ass@student.howest.be", false, false, null, null, null, null, null, false, "aa02e1e4-8744-45f4-b6a3-ebc805688e0c", false, "Judithvanass", "Judith van Ass" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5aa79098-9e74-492d-ad90-afc03cd9f81c");

            migrationBuilder.DropColumn(
                name: "Onderwerp",
                table: "Quizzen");

            migrationBuilder.AddColumn<string>(
                name: "Titel",
                table: "Quizzen",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Paswoord",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Naam", "Paswoord" },
                values: new object[] { "8e23ae18-a2a8-4609-acbe-3e387ea671bf", 0, "1a2cf89e-729a-4664-aef3-5546098fba6d", "Gebruiker", "Judith.van.ass@student.howest.be", false, false, null, null, null, "MijnP@sw00rd", null, false, "5ef10d8b-924d-4452-a827-17b76cceb147", false, "Judithvanass", "Judith van Ass", null });
        }
    }
}

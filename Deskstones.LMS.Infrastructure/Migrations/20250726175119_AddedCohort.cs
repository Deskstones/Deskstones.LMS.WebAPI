using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Deskstones.LMS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCohort : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.AddColumn<int>(
                name: "TeacherProfileId",
                table: "Subject",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cohort",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherProfileId = table.Column<int>(type: "integer", nullable: false),
                    SubjectId = table.Column<int>(type: "integer", nullable: false),
                    CohourtName = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    CohortAddress = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohort", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cohort_Subject_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cohort_TeacherProfile_TeacherProfileId",
                        column: x => x.TeacherProfileId,
                        principalTable: "TeacherProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subject_TeacherProfileId",
                table: "Subject",
                column: "TeacherProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Cohort_SubjectId",
                table: "Cohort",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Cohort_TeacherProfileId",
                table: "Cohort",
                column: "TeacherProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_TeacherProfile_TeacherProfileId",
                table: "Subject",
                column: "TeacherProfileId",
                principalTable: "TeacherProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_TeacherProfile_TeacherProfileId",
                table: "Subject");

            migrationBuilder.DropTable(
                name: "Cohort");

            migrationBuilder.DropIndex(
                name: "IX_Subject_TeacherProfileId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "TeacherProfileId",
                table: "Subject");

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    SubjectsId = table.Column<int>(type: "integer", nullable: false),
                    TeacherProfileId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => new { x.SubjectsId, x.TeacherProfileId });
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Subject_SubjectsId",
                        column: x => x.SubjectsId,
                        principalTable: "Subject",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_TeacherProfile_TeacherProfileId",
                        column: x => x.TeacherProfileId,
                        principalTable: "TeacherProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_TeacherProfileId",
                table: "TeacherSubjects",
                column: "TeacherProfileId");
        }
    }
}

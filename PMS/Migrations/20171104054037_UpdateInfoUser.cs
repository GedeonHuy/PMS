using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PMS.Migrations
{
    public partial class UpdateInfoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_CouncilErollment_Lecturer_LecturerID",
                table: "CouncilErollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Grade_GradeId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Group_GroupId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Council_CouncilId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Lecturer_LecturerId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Project_ProjectId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_CouncilId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_GradeId",
                table: "Enrollment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Desciption",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CouncilEnrollmentId",
                table: "Lecturer");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Lecturer");

            migrationBuilder.DropColumn(
                name: "CouncilId",
                table: "Group");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "LecturerID",
                table: "CouncilErollment",
                newName: "LecturerId");

            migrationBuilder.RenameIndex(
                name: "IX_CouncilErollment_LecturerID",
                table: "CouncilErollment",
                newName: "IX_CouncilErollment_LecturerId");

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Student",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LecturerId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Lecturer",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Group",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Group",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "MajorId",
                table: "Group",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QuarterId",
                table: "Group",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "isConfirm",
                table: "Group",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Group",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Enrollment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "GradeId",
                table: "Enrollment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "QuarterId",
                table: "Enrollment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Enrollment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "CouncilErollment",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CouncilRoleId",
                table: "CouncilErollment",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Percentage",
                table: "CouncilErollment",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isMark",
                table: "CouncilErollment",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Council",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CouncilRole",
                columns: table => new
                {
                    CouncilRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CouncilRoleName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouncilRole", x => x.CouncilRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Major",
                columns: table => new
                {
                    MajorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MajorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MajorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Major", x => x.MajorId);
                });

            migrationBuilder.CreateTable(
                name: "Quarter",
                columns: table => new
                {
                    QuarterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuarterEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuarterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuarterStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quarter", x => x.QuarterId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Student_MajorId",
                table: "Student",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LecturerId",
                table: "Project",
                column: "LecturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_MajorId",
                table: "Project",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Lecturer_MajorId",
                table: "Lecturer",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_MajorId",
                table: "Group",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_QuarterId",
                table: "Group",
                column: "QuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_GradeId",
                table: "Enrollment",
                column: "GradeId",
                unique: true,
                filter: "[GradeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_QuarterId",
                table: "Enrollment",
                column: "QuarterId");

            migrationBuilder.CreateIndex(
                name: "IX_CouncilErollment_CouncilRoleId",
                table: "CouncilErollment",
                column: "CouncilRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Council_GroupId",
                table: "Council",
                column: "GroupId",
                unique: true,
                filter: "[GroupId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_ApplicationUser_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_ApplicationUser_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_ApplicationUser_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_ApplicationUser_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Council_Group_GroupId",
                table: "Council",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CouncilErollment_CouncilRole_CouncilRoleId",
                table: "CouncilErollment",
                column: "CouncilRoleId",
                principalTable: "CouncilRole",
                principalColumn: "CouncilRoleId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CouncilErollment_Lecturer_LecturerId",
                table: "CouncilErollment",
                column: "LecturerId",
                principalTable: "Lecturer",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Grade_GradeId",
                table: "Enrollment",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Group_GroupId",
                table: "Enrollment",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Quarter_QuarterId",
                table: "Enrollment",
                column: "QuarterId",
                principalTable: "Quarter",
                principalColumn: "QuarterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Lecturer_LecturerId",
                table: "Group",
                column: "LecturerId",
                principalTable: "Lecturer",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Major_MajorId",
                table: "Group",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Project_ProjectId",
                table: "Group",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Quarter_QuarterId",
                table: "Group",
                column: "QuarterId",
                principalTable: "Quarter",
                principalColumn: "QuarterId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Lecturer_Major_MajorId",
                table: "Lecturer",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Lecturer_LecturerId",
                table: "Project",
                column: "LecturerId",
                principalTable: "Lecturer",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_Major_MajorId",
                table: "Project",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Student_Major_MajorId",
                table: "Student",
                column: "MajorId",
                principalTable: "Major",
                principalColumn: "MajorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_ApplicationUser_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_ApplicationUser_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_ApplicationUser_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_ApplicationUser_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Council_Group_GroupId",
                table: "Council");

            migrationBuilder.DropForeignKey(
                name: "FK_CouncilErollment_CouncilRole_CouncilRoleId",
                table: "CouncilErollment");

            migrationBuilder.DropForeignKey(
                name: "FK_CouncilErollment_Lecturer_LecturerId",
                table: "CouncilErollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Grade_GradeId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Group_GroupId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollment_Quarter_QuarterId",
                table: "Enrollment");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Lecturer_LecturerId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Major_MajorId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Project_ProjectId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Group_Quarter_QuarterId",
                table: "Group");

            migrationBuilder.DropForeignKey(
                name: "FK_Lecturer_Major_MajorId",
                table: "Lecturer");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Lecturer_LecturerId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_Major_MajorId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Student_Major_MajorId",
                table: "Student");

            migrationBuilder.DropTable(
                name: "CouncilRole");

            migrationBuilder.DropTable(
                name: "Major");

            migrationBuilder.DropTable(
                name: "Quarter");

            migrationBuilder.DropIndex(
                name: "IX_Student_MajorId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Project_LecturerId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_MajorId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Lecturer_MajorId",
                table: "Lecturer");

            migrationBuilder.DropIndex(
                name: "IX_Group_MajorId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_QuarterId",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_GradeId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_Enrollment_QuarterId",
                table: "Enrollment");

            migrationBuilder.DropIndex(
                name: "IX_CouncilErollment_CouncilRoleId",
                table: "CouncilErollment");

            migrationBuilder.DropIndex(
                name: "IX_Council_GroupId",
                table: "Council");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUser",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "LecturerId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Lecturer");

            migrationBuilder.DropColumn(
                name: "MajorId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "QuarterId",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "isConfirm",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "QuarterId",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Enrollment");

            migrationBuilder.DropColumn(
                name: "CouncilRoleId",
                table: "CouncilErollment");

            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "CouncilErollment");

            migrationBuilder.DropColumn(
                name: "isMark",
                table: "CouncilErollment");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Council");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "ApplicationUser");

            migrationBuilder.RenameTable(
                name: "ApplicationUser",
                newName: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "LecturerId",
                table: "CouncilErollment",
                newName: "LecturerID");

            migrationBuilder.RenameIndex(
                name: "IX_CouncilErollment_LecturerId",
                table: "CouncilErollment",
                newName: "IX_CouncilErollment_LecturerID");

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Desciption",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouncilEnrollmentId",
                table: "Lecturer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Lecturer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "Group",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LecturerId",
                table: "Group",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CouncilId",
                table: "Group",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "GroupId",
                table: "Enrollment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GradeId",
                table: "Enrollment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LecturerID",
                table: "CouncilErollment",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Group_CouncilId",
                table: "Group",
                column: "CouncilId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollment_GradeId",
                table: "Enrollment",
                column: "GradeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CouncilErollment_Lecturer_LecturerID",
                table: "CouncilErollment",
                column: "LecturerID",
                principalTable: "Lecturer",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Grade_GradeId",
                table: "Enrollment",
                column: "GradeId",
                principalTable: "Grade",
                principalColumn: "GradeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollment_Group_GroupId",
                table: "Enrollment",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Council_CouncilId",
                table: "Group",
                column: "CouncilId",
                principalTable: "Council",
                principalColumn: "CouncilId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Lecturer_LecturerId",
                table: "Group",
                column: "LecturerId",
                principalTable: "Lecturer",
                principalColumn: "LecturerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_Project_ProjectId",
                table: "Group",
                column: "ProjectId",
                principalTable: "Project",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

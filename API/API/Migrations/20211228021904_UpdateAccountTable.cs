using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class UpdateAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_tb_m_employees_Nik",
                table: "tb_m_accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_profiling_tb_m_accounts_Nik",
                table: "tb_m_profiling");

            migrationBuilder.RenameColumn(
                name: "Nik",
                table: "tb_m_profiling",
                newName: "NIK");

            migrationBuilder.RenameColumn(
                name: "Nik",
                table: "tb_m_employees",
                newName: "NIK");

            migrationBuilder.RenameColumn(
                name: "Nik",
                table: "tb_m_accounts",
                newName: "NIK");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredToken",
                table: "tb_m_accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "tb_m_accounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "OTP",
                table: "tb_m_accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accounts_tb_m_employees_NIK",
                table: "tb_m_accounts",
                column: "NIK",
                principalTable: "tb_m_employees",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_profiling_tb_m_accounts_NIK",
                table: "tb_m_profiling",
                column: "NIK",
                principalTable: "tb_m_accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_accounts_tb_m_employees_NIK",
                table: "tb_m_accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_profiling_tb_m_accounts_NIK",
                table: "tb_m_profiling");

            migrationBuilder.DropColumn(
                name: "ExpiredToken",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "tb_m_accounts");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "tb_m_profiling",
                newName: "Nik");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "tb_m_employees",
                newName: "Nik");

            migrationBuilder.RenameColumn(
                name: "NIK",
                table: "tb_m_accounts",
                newName: "Nik");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_accounts_tb_m_employees_Nik",
                table: "tb_m_accounts",
                column: "Nik",
                principalTable: "tb_m_employees",
                principalColumn: "Nik",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_profiling_tb_m_accounts_Nik",
                table: "tb_m_profiling",
                column: "Nik",
                principalTable: "tb_m_accounts",
                principalColumn: "Nik",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

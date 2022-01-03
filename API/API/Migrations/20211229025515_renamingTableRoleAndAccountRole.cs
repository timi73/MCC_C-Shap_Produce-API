using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class renamingTableRoleAndAccountRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountRoles_tb_m_accounts_AccountNIK",
                table: "AccountRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "tb_m_roles");

            migrationBuilder.RenameTable(
                name: "AccountRoles",
                newName: "tb_tr_accountroles");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_RoleId",
                table: "tb_tr_accountroles",
                newName: "IX_tb_tr_accountroles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_AccountRoles_AccountNIK",
                table: "tb_tr_accountroles",
                newName: "IX_tb_tr_accountroles_AccountNIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_roles",
                table: "tb_m_roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_tr_accountroles",
                table: "tb_tr_accountroles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountroles_tb_m_accounts_AccountNIK",
                table: "tb_tr_accountroles",
                column: "AccountNIK",
                principalTable: "tb_m_accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_tr_accountroles_tb_m_roles_RoleId",
                table: "tb_tr_accountroles",
                column: "RoleId",
                principalTable: "tb_m_roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountroles_tb_m_accounts_AccountNIK",
                table: "tb_tr_accountroles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_tr_accountroles_tb_m_roles_RoleId",
                table: "tb_tr_accountroles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_tr_accountroles",
                table: "tb_tr_accountroles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_roles",
                table: "tb_m_roles");

            migrationBuilder.RenameTable(
                name: "tb_tr_accountroles",
                newName: "AccountRoles");

            migrationBuilder.RenameTable(
                name: "tb_m_roles",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_accountroles_RoleId",
                table: "AccountRoles",
                newName: "IX_AccountRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_tr_accountroles_AccountNIK",
                table: "AccountRoles",
                newName: "IX_AccountRoles_AccountNIK");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountRoles",
                table: "AccountRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_Roles_RoleId",
                table: "AccountRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountRoles_tb_m_accounts_AccountNIK",
                table: "AccountRoles",
                column: "AccountNIK",
                principalTable: "tb_m_accounts",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

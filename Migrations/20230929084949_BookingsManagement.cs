using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking_Api.Migrations
{
    public partial class BookingsManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                table: "tb_tr_bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                table: "tb_m_universities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                table: "tb_m_rooms",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                table: "tb_m_roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                table: "tb_m_employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                table: "tb_m_educations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                table: "tb_m_accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                table: "tb_m_account_roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "modified_date",
                table: "tb_tr_bookings");

            migrationBuilder.DropColumn(
                name: "modified_date",
                table: "tb_m_universities");

            migrationBuilder.DropColumn(
                name: "modified_date",
                table: "tb_m_rooms");

            migrationBuilder.DropColumn(
                name: "modified_date",
                table: "tb_m_roles");

            migrationBuilder.DropColumn(
                name: "modified_date",
                table: "tb_m_employees");

            migrationBuilder.DropColumn(
                name: "modified_date",
                table: "tb_m_educations");

            migrationBuilder.DropColumn(
                name: "modified_date",
                table: "tb_m_accounts");

            migrationBuilder.DropColumn(
                name: "modified_date",
                table: "tb_m_account_roles");
        }
    }
}

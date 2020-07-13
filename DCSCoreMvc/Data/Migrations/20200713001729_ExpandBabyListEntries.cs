using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DCSCoreMvc.Data.Migrations
{
    public partial class ExpandBabyListEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "BabyListEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "BabyListEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nr",
                table: "BabyListEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "BabyListEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "BabyListEntries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "BabyListEntries");

            migrationBuilder.DropColumn(
                name: "City",
                table: "BabyListEntries");

            migrationBuilder.DropColumn(
                name: "Nr",
                table: "BabyListEntries");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "BabyListEntries");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "BabyListEntries");
        }
    }
}

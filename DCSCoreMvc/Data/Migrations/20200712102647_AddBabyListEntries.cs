using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DCSCoreMvc.Data.Migrations
{
  public partial class AddBabyListEntries : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "BabyListEntries",
          columns: table => new
          {
            Id = table.Column<int>(nullable: false)
                  .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
            CreatedDate = table.Column<DateTimeOffset>(nullable: false),
            Deleted = table.Column<bool>(nullable: false),
            Email = table.Column<string>(nullable: true),
            LastModifiedDate = table.Column<DateTimeOffset>(nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_BabyListEntries", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {

      migrationBuilder.DropTable(
          name: "BabyListEntries");

    }
  }
}

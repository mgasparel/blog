using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace blog.Migrations
{
    public partial class postupdatednonnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Posts SET Updated = Created WHERE Updated IS NULL");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}

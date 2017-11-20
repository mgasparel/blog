using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace blog.Migrations
{
    public partial class add_slug_to_post : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);

            //Will convert the majority of cases, but not at all perfect.
            migrationBuilder.Sql("UPDATE Posts SET Slug = LOWER(REPLACE(Title, ' ', '-'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Posts");
        }
    }
}

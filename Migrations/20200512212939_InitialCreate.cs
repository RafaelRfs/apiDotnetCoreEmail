using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

namespace apiDotnetCoreEmail.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             string driverdb = Environment.GetEnvironmentVariable("DRIVERDB");

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    id =
                    ( driverdb != "Postgresql" ?
                     table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                        :
                    table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                    )    
                    ,
                    from = table.Column<string>(nullable: true),
                    adress = table.Column<string>(nullable: true),
                    to = table.Column<string>(nullable: true),
                    msg = table.Column<string>(nullable: true),
                    options = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.id);
                });
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");
        }
    }
}

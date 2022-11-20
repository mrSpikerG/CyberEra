using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberEra_Server_new.Migrations
{
    public partial class m1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    LogContext = table.Column<string>(type: "ntext", nullable: true),
                    TimeCreation = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nchar(500)", fixedLength: true, maxLength: 500, nullable: true),
                    Stars = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RoleName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "ShopItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<float>(type: "real", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: true),
                    ImageSrc = table.Column<string>(type: "ntext", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Role",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Computers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computers_Zones",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuyedFood",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: true),
                    ShopItemId = table.Column<int>(type: "int", nullable: true),
                    ComputerId = table.Column<int>(type: "int", nullable: true),
                    TimeCreation = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeForPlaying = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Таблица1_Computers",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Таблица1_ShopItems",
                        column: x => x.ShopItemId,
                        principalTable: "ShopItems",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComputerApps",
                columns: table => new
                {
                    ComputerId = table.Column<int>(type: "int", nullable: true),
                    AppName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    AppVersion = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ComputerApps_Computers",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComputersIp",
                columns: table => new
                {
                    ComputerId = table.Column<int>(type: "int", nullable: true),
                    IpAddress = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ComputersIp_Computers",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComputersLastActivity",
                columns: table => new
                {
                    ComputerId = table.Column<int>(type: "int", nullable: true),
                    TimeActivity = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Table_1_Computers",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ComputersPasswords",
                columns: table => new
                {
                    ComputerId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    TimeCreation = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeForPlaying = table.Column<DateTime>(type: "datetime", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_ComputersPasswords_Computers",
                        column: x => x.ComputerId,
                        principalTable: "Computers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComputersPasswords_Users",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BuyedFood_ComputerId",
                table: "BuyedFood",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyedFood_ShopItemId",
                table: "BuyedFood",
                column: "ShopItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputerApps_ComputerId",
                table: "ComputerApps",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_ZoneId",
                table: "Computers",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputersIp_ComputerId",
                table: "ComputersIp",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputersLastActivity_ComputerId",
                table: "ComputersLastActivity",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputersPasswords_ComputerId",
                table: "ComputersPasswords",
                column: "ComputerId");

            migrationBuilder.CreateIndex(
                name: "IX_ComputersPasswords_UserId",
                table: "ComputersPasswords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuyedFood");

            migrationBuilder.DropTable(
                name: "ComputerApps");

            migrationBuilder.DropTable(
                name: "ComputersIp");

            migrationBuilder.DropTable(
                name: "ComputersLastActivity");

            migrationBuilder.DropTable(
                name: "ComputersPasswords");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "ShopItems");

            migrationBuilder.DropTable(
                name: "Computers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}

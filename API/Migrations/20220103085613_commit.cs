using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class commit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_status_code",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_status_code", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_ticket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Review = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnicalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ticket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_ticket_tb_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_employeee",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_employeee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_employeee_tb_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tb_roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Convertation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Convertation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_Convertation_tb_employeee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_employeee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_Convertation_tb_ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "tb_ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_history",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    StatusCodeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_history", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_history_tb_employeee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tb_employeee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_history_tb_status_code_StatusCodeId",
                        column: x => x.StatusCodeId,
                        principalTable: "tb_status_code",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_history_tb_ticket_TicketId",
                        column: x => x.TicketId,
                        principalTable: "tb_ticket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_Convertation_EmployeeId",
                table: "tb_Convertation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Convertation_TicketId",
                table: "tb_Convertation",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_employeee_RoleId",
                table: "tb_employeee",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_history_EmployeeId",
                table: "tb_history",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_history_StatusCodeId",
                table: "tb_history",
                column: "StatusCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_history_TicketId",
                table: "tb_history",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_ticket_CategoryId",
                table: "tb_ticket",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Convertation");

            migrationBuilder.DropTable(
                name: "tb_history");

            migrationBuilder.DropTable(
                name: "tb_employeee");

            migrationBuilder.DropTable(
                name: "tb_status_code");

            migrationBuilder.DropTable(
                name: "tb_ticket");

            migrationBuilder.DropTable(
                name: "tb_roles");

            migrationBuilder.DropTable(
                name: "tb_category");
        }
    }
}

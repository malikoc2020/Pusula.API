using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class YearMonthPayroll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Month",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Month", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Year",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Year", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payroll",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Overtime = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payroll_Month_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Month",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payroll_Year_YearId",
                        column: x => x.YearId,
                        principalTable: "Year",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payroll_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PayrollSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollSetting", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollSetting_Month_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Month",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayrollSetting_Year_YearId",
                        column: x => x.YearId,
                        principalTable: "Year",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PayrollTemp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    YearId = table.Column<int>(type: "int", nullable: false),
                    MonthId = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Overtime = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollTemp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayrollTemp_Month_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Month",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayrollTemp_Year_YearId",
                        column: x => x.YearId,
                        principalTable: "Year",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PayrollTemp_user_UserId",
                        column: x => x.UserId,
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_MonthId",
                table: "Payroll",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_UserId",
                table: "Payroll",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_YearId",
                table: "Payroll",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSetting_MonthId",
                table: "PayrollSetting",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollSetting_YearId",
                table: "PayrollSetting",
                column: "YearId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollTemp_MonthId",
                table: "PayrollTemp",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollTemp_UserId",
                table: "PayrollTemp",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollTemp_YearId",
                table: "PayrollTemp",
                column: "YearId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payroll");

            migrationBuilder.DropTable(
                name: "PayrollSetting");

            migrationBuilder.DropTable(
                name: "PayrollTemp");

            migrationBuilder.DropTable(
                name: "Month");

            migrationBuilder.DropTable(
                name: "Year");
        }
    }
}

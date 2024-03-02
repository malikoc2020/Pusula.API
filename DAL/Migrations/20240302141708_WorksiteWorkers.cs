using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class WorksiteWorkers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorksiteWorker_WorksiteWorkerType_WorkSiteWorkeTypeId",
                table: "WorksiteWorker");

            migrationBuilder.RenameColumn(
                name: "WorkSiteWorkeTypeId",
                table: "WorksiteWorker",
                newName: "WorksiteWorkerTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorksiteWorker_WorkSiteWorkeTypeId",
                table: "WorksiteWorker",
                newName: "IX_WorksiteWorker_WorksiteWorkerTypeId");

            migrationBuilder.AddColumn<int>(
                name: "WorkersiteId",
                table: "WorksiteWorker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_WorksiteWorker_WorksiteWorkerType_WorksiteWorkerTypeId",
                table: "WorksiteWorker",
                column: "WorksiteWorkerTypeId",
                principalTable: "WorksiteWorkerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorksiteWorker_WorksiteWorkerType_WorksiteWorkerTypeId",
                table: "WorksiteWorker");

            migrationBuilder.DropColumn(
                name: "WorkersiteId",
                table: "WorksiteWorker");

            migrationBuilder.RenameColumn(
                name: "WorksiteWorkerTypeId",
                table: "WorksiteWorker",
                newName: "WorkSiteWorkeTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_WorksiteWorker_WorksiteWorkerTypeId",
                table: "WorksiteWorker",
                newName: "IX_WorksiteWorker_WorkSiteWorkeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorksiteWorker_WorksiteWorkerType_WorkSiteWorkeTypeId",
                table: "WorksiteWorker",
                column: "WorkSiteWorkeTypeId",
                principalTable: "WorksiteWorkerType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

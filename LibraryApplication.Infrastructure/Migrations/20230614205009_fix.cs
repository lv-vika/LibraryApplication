using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApplication.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTransfers_Discounts_DiscountId",
                table: "BookTransfers");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "BookTransfers",
                newName: "DiscountEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_BookTransfers_DiscountId",
                table: "BookTransfers",
                newName: "IX_BookTransfers_DiscountEntityId");

            migrationBuilder.AlterColumn<int>(
                name: "UserCategoryId",
                table: "Discounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTransfers_Discounts_DiscountEntityId",
                table: "BookTransfers",
                column: "DiscountEntityId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTransfers_Discounts_DiscountEntityId",
                table: "BookTransfers");

            migrationBuilder.RenameColumn(
                name: "DiscountEntityId",
                table: "BookTransfers",
                newName: "DiscountId");

            migrationBuilder.RenameIndex(
                name: "IX_BookTransfers_DiscountEntityId",
                table: "BookTransfers",
                newName: "IX_BookTransfers_DiscountId");

            migrationBuilder.AlterColumn<int>(
                name: "UserCategoryId",
                table: "Discounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_BookTransfers_Discounts_DiscountId",
                table: "BookTransfers",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id");
        }
    }
}

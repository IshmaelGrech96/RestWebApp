using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Data.Migrations
{
    public partial class UpdatingCartProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartProducts",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ShoppingCartProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "CartProductId",
                table: "ShoppingCartProducts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartProducts",
                table: "ShoppingCartProducts",
                column: "CartProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartProducts",
                table: "ShoppingCartProducts");

            migrationBuilder.DropColumn(
                name: "CartProductId",
                table: "ShoppingCartProducts");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "ShoppingCartProducts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartProducts",
                table: "ShoppingCartProducts",
                column: "ItemId");
        }
    }
}

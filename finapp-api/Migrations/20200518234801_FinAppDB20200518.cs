using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinAppApi.Migrations
{
    public partial class FinAppDB20200518 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InsertedDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Flag = table.Column<int>(nullable: false),
                    Limit = table.Column<decimal>(nullable: false),
                    LastDateInvoice = table.Column<int>(nullable: false),
                    DueDateInvoice = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    WalletId = table.Column<int>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCards_client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CreditCards_wallet_WalletId",
                        column: x => x.WalletId,
                        principalTable: "wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_ClientId",
                table: "CreditCards",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_WalletId",
                table: "CreditCards",
                column: "WalletId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCards");
        }
    }
}

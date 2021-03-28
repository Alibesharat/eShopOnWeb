using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microsoft.eShopWeb.Web.Parbad.Migrations
{
    public partial class payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "parbad");

            migrationBuilder.CreateTable(
                name: "payment",
                schema: "parbad",
                columns: table => new
                {
                    payment_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tracking_number = table.Column<long>(nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    token = table.Column<string>(nullable: false),
                    transaction_code = table.Column<string>(nullable: true),
                    gateway_name = table.Column<string>(maxLength: 20, nullable: false),
                    gateway_account_name = table.Column<string>(nullable: true),
                    is_completed = table.Column<bool>(nullable: false),
                    is_paid = table.Column<bool>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payment_id", x => x.payment_id);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                schema: "parbad",
                columns: table => new
                {
                    transaction_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    type = table.Column<byte>(nullable: false),
                    is_succeed = table.Column<bool>(nullable: false),
                    message = table.Column<string>(nullable: true),
                    additional_data = table.Column<string>(nullable: true),
                    PaymentId = table.Column<long>(nullable: false),
                    created_on = table.Column<DateTime>(nullable: false),
                    updated_on = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transaction_id", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_transaction_payment_PaymentId",
                        column: x => x.PaymentId,
                        principalSchema: "parbad",
                        principalTable: "payment",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_payment_token",
                schema: "parbad",
                table: "payment",
                column: "token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payment_tracking_number",
                schema: "parbad",
                table: "payment",
                column: "tracking_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_PaymentId",
                schema: "parbad",
                table: "transaction",
                column: "PaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transaction",
                schema: "parbad");

            migrationBuilder.DropTable(
                name: "payment",
                schema: "parbad");
        }
    }
}

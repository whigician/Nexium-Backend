using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexium.API.Migrations
{
    /// <inheritdoc />
    public partial class BusinessTypesTranslationsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_business_types_translations_business_type_id",
                table: "business_types_translations");

            migrationBuilder.CreateIndex(
                name: "ix_business_types_translations_business_type_id_language_code",
                table: "business_types_translations",
                columns: new[] { "business_type_id", "language_code" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_business_types_translations_business_type_id_language_code",
                table: "business_types_translations");

            migrationBuilder.CreateIndex(
                name: "ix_business_types_translations_business_type_id",
                table: "business_types_translations",
                column: "business_type_id");
        }
    }
}

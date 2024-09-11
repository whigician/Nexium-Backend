using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Nexium.API.Migrations
{
    /// <inheritdoc />
    public partial class BusinessTypesTranslations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "id",
                table: "industry_translations",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "business_types_translations",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    language_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    translated_label = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    business_type_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_types_translations", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_types_translations_business_types_business_type_id",
                        column: x => x.business_type_id,
                        principalTable: "business_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_business_types_translations_business_type_id",
                table: "business_types_translations",
                column: "business_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "business_types_translations");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "industry_translations",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}

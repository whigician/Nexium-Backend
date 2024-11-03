using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Nexium.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address_types",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "business_statuses",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_statuses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "business_types",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "contact_types",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contact_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currencies",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currencies", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "identifier_types",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identifier_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "industries",
                columns: table => new
                {
                    id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_industries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "city",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    country_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city", x => x.id);
                    table.ForeignKey(
                        name: "fk_city_country_country_id",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    alias = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    establishment_year = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    start_operating_hour = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    end_operating_hour = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    employees_count = table.Column<int>(type: "integer", nullable: false),
                    logo_path = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    fiscal_year_start_month = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    language_code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    currency_id = table.Column<byte>(type: "smallint", nullable: true),
                    language_id = table.Column<byte>(type: "smallint", nullable: false),
                    business_type_id = table.Column<byte>(type: "smallint", nullable: false),
                    business_status_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_business_statuses_business_status_id",
                        column: x => x.business_status_id,
                        principalTable: "business_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_business_types_business_type_id",
                        column: x => x.business_type_id,
                        principalTable: "business_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_business_languages_language_code",
                        column: x => x.language_code,
                        principalTable: "languages",
                        principalColumn: "code");
                });

            migrationBuilder.CreateTable(
                name: "translation_mappings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    language_code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    entity_type_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    entity_id = table.Column<long>(type: "bigint", nullable: false),
                    attribute_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    translated_text = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_translation_mappings", x => x.id);
                    table.ForeignKey(
                        name: "fk_translation_mappings_languages_language_code",
                        column: x => x.language_code,
                        principalTable: "languages",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_address",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    zip_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    longitude = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    latitude = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: true),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    address_type_id = table.Column<byte>(type: "smallint", nullable: false),
                    business_id = table.Column<long>(type: "bigint", nullable: false),
                    city_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_address", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_address_address_types_address_type_id",
                        column: x => x.address_type_id,
                        principalTable: "address_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_address_business_business_id",
                        column: x => x.business_id,
                        principalTable: "business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_address_city_city_id",
                        column: x => x.city_id,
                        principalTable: "city",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_contact",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    responsible_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    responsible_position = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: true),
                    phone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    contact_type_id = table.Column<byte>(type: "smallint", nullable: false),
                    business_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_contact", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_contact_business_business_id",
                        column: x => x.business_id,
                        principalTable: "business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_contact_contact_types_contact_type_id",
                        column: x => x.contact_type_id,
                        principalTable: "contact_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_identifier",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    business_id = table.Column<long>(type: "bigint", nullable: false),
                    identifier_type_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_identifier", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_identifier_business_business_id",
                        column: x => x.business_id,
                        principalTable: "business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_identifier_identifier_types_identifier_type_id",
                        column: x => x.identifier_type_id,
                        principalTable: "identifier_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_industry",
                columns: table => new
                {
                    businesses_id = table.Column<long>(type: "bigint", nullable: false),
                    industries_id = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_industry", x => new { x.businesses_id, x.industries_id });
                    table.ForeignKey(
                        name: "fk_business_industry_business_businesses_id",
                        column: x => x.businesses_id,
                        principalTable: "business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_industry_industries_industries_id",
                        column: x => x.industries_id,
                        principalTable: "industries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_relationship",
                columns: table => new
                {
                    business_id = table.Column<long>(type: "bigint", nullable: false),
                    related_business_id = table.Column<long>(type: "bigint", nullable: false),
                    is_retailer = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_relationship", x => new { x.business_id, x.related_business_id });
                    table.ForeignKey(
                        name: "fk_business_relationship_business_business_id",
                        column: x => x.business_id,
                        principalTable: "business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_relationship_business_related_business_id",
                        column: x => x.related_business_id,
                        principalTable: "business",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "target_markets",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    business_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_target_markets", x => x.id);
                    table.ForeignKey(
                        name: "fk_target_markets_business_business_id",
                        column: x => x.business_id,
                        principalTable: "business",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "languages",
                columns: new[] { "code", "name" },
                values: new object[,]
                {
                    { "ar", "العربية" },
                    { "en", "English" },
                    { "fr", "Français" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_business_business_status_id",
                table: "business",
                column: "business_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_business_type_id",
                table: "business",
                column: "business_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_currency_id",
                table: "business",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_language_code",
                table: "business",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "ix_business_address_address_type_id",
                table: "business_address",
                column: "address_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_address_business_id",
                table: "business_address",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_address_city_id",
                table: "business_address",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_contact_business_id",
                table: "business_contact",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_contact_contact_type_id",
                table: "business_contact",
                column: "contact_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_identifier_business_id",
                table: "business_identifier",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_identifier_identifier_type_id",
                table: "business_identifier",
                column: "identifier_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_industry_industries_id",
                table: "business_industry",
                column: "industries_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_relationship_related_business_id",
                table: "business_relationship",
                column: "related_business_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_country_id",
                table: "city",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_target_markets_business_id",
                table: "target_markets",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_translation_mappings_entity_id_entity_type_name_attribute_n",
                table: "translation_mappings",
                columns: new[] { "entity_id", "entity_type_name", "attribute_name", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_translation_mappings_language_code",
                table: "translation_mappings",
                column: "language_code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "business_address");

            migrationBuilder.DropTable(
                name: "business_contact");

            migrationBuilder.DropTable(
                name: "business_identifier");

            migrationBuilder.DropTable(
                name: "business_industry");

            migrationBuilder.DropTable(
                name: "business_relationship");

            migrationBuilder.DropTable(
                name: "target_markets");

            migrationBuilder.DropTable(
                name: "translation_mappings");

            migrationBuilder.DropTable(
                name: "address_types");

            migrationBuilder.DropTable(
                name: "city");

            migrationBuilder.DropTable(
                name: "contact_types");

            migrationBuilder.DropTable(
                name: "identifier_types");

            migrationBuilder.DropTable(
                name: "industries");

            migrationBuilder.DropTable(
                name: "business");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "business_statuses");

            migrationBuilder.DropTable(
                name: "business_types");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "languages");
        }
    }
}

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
                name: "app_sections",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    parent_app_section_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_app_sections", x => x.id);
                    table.ForeignKey(
                        name: "fk_app_sections_app_sections_parent_app_section_id",
                        column: x => x.parent_app_section_id,
                        principalTable: "app_sections",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "business_link_types",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    original_logo_url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    resized_logo_url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_link_types", x => x.id);
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
                name: "countries",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
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
                name: "employee_positions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_positions", x => x.id);
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
                name: "permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    permission_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "person_identifier_types",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_identifier_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "subscription_plans",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    monthly_price_per_user = table.Column<decimal>(type: "numeric", nullable: false),
                    yearly_price_per_user = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscription_plans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "target_markets",
                columns: table => new
                {
                    id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    label = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_target_markets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    country_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cities", x => x.id);
                    table.ForeignKey(
                        name: "fk_cities_countries_country_id",
                        column: x => x.country_id,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "businesses",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    alias = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    establishment_year = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: true),
                    start_operating_hour = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    end_operating_hour = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: true),
                    about_description_markup = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: true),
                    employees_count = table.Column<int>(type: "integer", nullable: false),
                    logo_url = table.Column<string>(type: "character varying(220)", maxLength: 220, nullable: true),
                    fiscal_year_start_month = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_registration_completed = table.Column<bool>(type: "boolean", nullable: false),
                    achieved_step_index = table.Column<byte>(type: "smallint", nullable: false),
                    terms_accepted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    language_code = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: true),
                    currency_id = table.Column<byte>(type: "smallint", nullable: true),
                    language_id = table.Column<byte>(type: "smallint", nullable: false),
                    business_type_id = table.Column<byte>(type: "smallint", nullable: false),
                    current_status_id = table.Column<byte>(type: "smallint", nullable: false),
                    is_registered = table.Column<bool>(type: "boolean", nullable: false),
                    address_type_id = table.Column<byte>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_businesses", x => x.id);
                    table.ForeignKey(
                        name: "fk_businesses_address_types_address_type_id",
                        column: x => x.address_type_id,
                        principalTable: "address_types",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_businesses_business_statuses_current_status_id",
                        column: x => x.current_status_id,
                        principalTable: "business_statuses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_businesses_business_types_business_type_id",
                        column: x => x.business_type_id,
                        principalTable: "business_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_businesses_currencies_currency_id",
                        column: x => x.currency_id,
                        principalTable: "currencies",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_businesses_languages_language_code",
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
                name: "business_addresses",
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
                    city_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_addresses", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_addresses_address_types_address_type_id",
                        column: x => x.address_type_id,
                        principalTable: "address_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_addresses_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_addresses_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_contacts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    responsible_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    employee_position_id = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("pk_business_contacts", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_contacts_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_contacts_contact_types_contact_type_id",
                        column: x => x.contact_type_id,
                        principalTable: "contact_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_contacts_employee_positions_employee_position_id",
                        column: x => x.employee_position_id,
                        principalTable: "employee_positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_identifiers",
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
                    table.PrimaryKey("pk_business_identifiers", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_identifiers_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_identifiers_identifier_types_identifier_type_id",
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
                        name: "fk_business_industry_businesses_businesses_id",
                        column: x => x.businesses_id,
                        principalTable: "businesses",
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
                name: "business_links",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    business_id = table.Column<long>(type: "bigint", nullable: false),
                    business_link_type_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_links", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_links_business_link_types_business_link_type_id",
                        column: x => x.business_link_type_id,
                        principalTable: "business_link_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_links_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_members",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    city_id = table.Column<int>(type: "integer", nullable: true),
                    photo_url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    business_id = table.Column<long>(type: "bigint", nullable: false),
                    business_owner_id = table.Column<long>(type: "bigint", nullable: true),
                    employee_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_members", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_members_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_members_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "business_relationships",
                columns: table => new
                {
                    supplier_id = table.Column<long>(type: "bigint", nullable: false),
                    retailer_id = table.Column<long>(type: "bigint", nullable: false),
                    business_relationship_type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_relationships", x => new { x.supplier_id, x.retailer_id, x.business_relationship_type });
                    table.ForeignKey(
                        name: "fk_business_relationships_businesses_retailer_id",
                        column: x => x.retailer_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_business_relationships_businesses_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "business_roles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    business_id = table.Column<long>(type: "bigint", nullable: false),
                    role_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_roles_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_target_market",
                columns: table => new
                {
                    businesses_id = table.Column<long>(type: "bigint", nullable: false),
                    target_markets_id = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_target_market", x => new { x.businesses_id, x.target_markets_id });
                    table.ForeignKey(
                        name: "fk_business_target_market_businesses_businesses_id",
                        column: x => x.businesses_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_target_market_target_markets_target_markets_id",
                        column: x => x.target_markets_id,
                        principalTable: "target_markets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    business_id = table.Column<long>(type: "bigint", nullable: false),
                    subscription_plan_id = table.Column<int>(type: "integer", nullable: false),
                    starts_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ends_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    auto_renewal = table.Column<bool>(type: "boolean", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "fk_subscriptions_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_subscriptions_subscription_plans_subscription_plan_id",
                        column: x => x.subscription_plan_id,
                        principalTable: "subscription_plans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_owners",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    business_member_id = table.Column<long>(type: "bigint", nullable: false),
                    owner_ship_percentage = table.Column<float>(type: "real", nullable: false),
                    owner_ship_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    is_primary_owner = table.Column<bool>(type: "boolean", nullable: false),
                    user_account_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_owners", x => x.id);
                    table.ForeignKey(
                        name: "fk_business_owners_business_members_business_member_id",
                        column: x => x.business_member_id,
                        principalTable: "business_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    business_member_id = table.Column<long>(type: "bigint", nullable: false),
                    hiring_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    departure_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_account_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employees", x => x.id);
                    table.ForeignKey(
                        name: "fk_employees_business_members_business_member_id",
                        column: x => x.business_member_id,
                        principalTable: "business_members",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "person_identifiers",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    value = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    document_url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    person_identifier_type_id = table.Column<byte>(type: "smallint", nullable: false),
                    business_member_id = table.Column<long>(type: "bigint", nullable: true),
                    internal_person_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_person_identifiers", x => x.id);
                    table.ForeignKey(
                        name: "fk_person_identifiers_business_members_business_member_id",
                        column: x => x.business_member_id,
                        principalTable: "business_members",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_person_identifiers_person_identifier_types_person_identifie",
                        column: x => x.person_identifier_type_id,
                        principalTable: "person_identifier_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    app_section_id = table.Column<int>(type: "integer", nullable: false),
                    business_role_id = table.Column<long>(type: "bigint", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false),
                    allowed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_permissions_app_sections_app_section_id",
                        column: x => x.app_section_id,
                        principalTable: "app_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permissions_business_roles_business_role_id",
                        column: x => x.business_role_id,
                        principalTable: "business_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_role_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_employee_position",
                columns: table => new
                {
                    employees_id = table.Column<long>(type: "bigint", nullable: false),
                    positions_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_employee_employee_position", x => new { x.employees_id, x.positions_id });
                    table.ForeignKey(
                        name: "fk_employee_employee_position_employee_positions_positions_id",
                        column: x => x.positions_id,
                        principalTable: "employee_positions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_employee_employee_position_employees_employees_id",
                        column: x => x.employees_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_accounts",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    backup_email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    hashed_password = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    is_two_factor_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    two_factor_method = table.Column<int>(type: "integer", nullable: false),
                    password_updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    last_login = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    failed_login_attempts = table.Column<byte>(type: "smallint", nullable: false),
                    account_locked_until = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    profile_picture_url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    email_verification_code = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    email_verification_expiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    phone_verification_code = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    phone_verification_expiry = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    email_verified = table.Column<bool>(type: "boolean", nullable: false),
                    phone_number_verified = table.Column<bool>(type: "boolean", nullable: false),
                    business_owner_id = table.Column<long>(type: "bigint", nullable: true),
                    employee_id = table.Column<long>(type: "bigint", nullable: true),
                    business_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_accounts", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_accounts_business_owners_business_owner_id",
                        column: x => x.business_owner_id,
                        principalTable: "business_owners",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_user_accounts_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_accounts_employees_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "business_role_user_account",
                columns: table => new
                {
                    business_roles_id = table.Column<long>(type: "bigint", nullable: false),
                    user_accounts_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_business_role_user_account", x => new { x.business_roles_id, x.user_accounts_id });
                    table.ForeignKey(
                        name: "fk_business_role_user_account_business_roles_business_roles_id",
                        column: x => x.business_roles_id,
                        principalTable: "business_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_business_role_user_account_user_accounts_user_accounts_id",
                        column: x => x.user_accounts_id,
                        principalTable: "user_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_account_activities",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    operation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    operation = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    entity_type_name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    entity_id = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    user_account_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_account_activities", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_account_activities_user_accounts_user_account_id",
                        column: x => x.user_account_id,
                        principalTable: "user_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_account_permissions",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    app_section_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false),
                    user_account_id = table.Column<long>(type: "bigint", nullable: false),
                    allowed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_account_permissions", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_account_permissions_app_sections_app_section_id",
                        column: x => x.app_section_id,
                        principalTable: "app_sections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_account_permissions_permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_account_permissions_user_accounts_user_account_id",
                        column: x => x.user_account_id,
                        principalTable: "user_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "ix_app_sections_parent_app_section_id",
                table: "app_sections",
                column: "parent_app_section_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_addresses_address_type_id",
                table: "business_addresses",
                column: "address_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_addresses_business_id",
                table: "business_addresses",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_addresses_city_id",
                table: "business_addresses",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_contacts_business_id",
                table: "business_contacts",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_contacts_contact_type_id",
                table: "business_contacts",
                column: "contact_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_contacts_employee_position_id",
                table: "business_contacts",
                column: "employee_position_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_identifiers_business_id",
                table: "business_identifiers",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_identifiers_identifier_type_id",
                table: "business_identifiers",
                column: "identifier_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_industry_industries_id",
                table: "business_industry",
                column: "industries_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_links_business_id",
                table: "business_links",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_links_business_link_type_id",
                table: "business_links",
                column: "business_link_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_members_business_id",
                table: "business_members",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_members_city_id",
                table: "business_members",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_owners_business_member_id",
                table: "business_owners",
                column: "business_member_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_business_relationships_retailer_id",
                table: "business_relationships",
                column: "retailer_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_role_user_account_user_accounts_id",
                table: "business_role_user_account",
                column: "user_accounts_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_roles_business_id",
                table: "business_roles",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_roles_role_id",
                table: "business_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_business_target_market_target_markets_id",
                table: "business_target_market",
                column: "target_markets_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_address_type_id",
                table: "businesses",
                column: "address_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_business_type_id",
                table: "businesses",
                column: "business_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_currency_id",
                table: "businesses",
                column: "currency_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_current_status_id",
                table: "businesses",
                column: "current_status_id");

            migrationBuilder.CreateIndex(
                name: "ix_businesses_language_code",
                table: "businesses",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "ix_cities_country_id",
                table: "cities",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_employee_employee_position_positions_id",
                table: "employee_employee_position",
                column: "positions_id");

            migrationBuilder.CreateIndex(
                name: "ix_employees_business_member_id",
                table: "employees",
                column: "business_member_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_person_identifiers_business_member_id",
                table: "person_identifiers",
                column: "business_member_id");

            migrationBuilder.CreateIndex(
                name: "ix_person_identifiers_person_identifier_type_id",
                table: "person_identifiers",
                column: "person_identifier_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_app_section_id",
                table: "role_permissions",
                column: "app_section_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_business_role_id",
                table: "role_permissions",
                column: "business_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_role_permissions_permission_id",
                table: "role_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscriptions_business_id",
                table: "subscriptions",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_subscriptions_subscription_plan_id",
                table: "subscriptions",
                column: "subscription_plan_id");

            migrationBuilder.CreateIndex(
                name: "ix_translation_mappings_entity_id_entity_type_name_attribute_n",
                table: "translation_mappings",
                columns: new[] { "entity_id", "entity_type_name", "attribute_name", "language_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_translation_mappings_language_code",
                table: "translation_mappings",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "ix_user_account_activities_user_account_id",
                table: "user_account_activities",
                column: "user_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_account_permissions_app_section_id",
                table: "user_account_permissions",
                column: "app_section_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_account_permissions_permission_id",
                table: "user_account_permissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_account_permissions_user_account_id",
                table: "user_account_permissions",
                column: "user_account_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_accounts_business_id",
                table: "user_accounts",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_accounts_business_owner_id",
                table: "user_accounts",
                column: "business_owner_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_accounts_employee_id",
                table: "user_accounts",
                column: "employee_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "business_addresses");

            migrationBuilder.DropTable(
                name: "business_contacts");

            migrationBuilder.DropTable(
                name: "business_identifiers");

            migrationBuilder.DropTable(
                name: "business_industry");

            migrationBuilder.DropTable(
                name: "business_links");

            migrationBuilder.DropTable(
                name: "business_relationships");

            migrationBuilder.DropTable(
                name: "business_role_user_account");

            migrationBuilder.DropTable(
                name: "business_target_market");

            migrationBuilder.DropTable(
                name: "employee_employee_position");

            migrationBuilder.DropTable(
                name: "person_identifiers");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "translation_mappings");

            migrationBuilder.DropTable(
                name: "user_account_activities");

            migrationBuilder.DropTable(
                name: "user_account_permissions");

            migrationBuilder.DropTable(
                name: "contact_types");

            migrationBuilder.DropTable(
                name: "identifier_types");

            migrationBuilder.DropTable(
                name: "industries");

            migrationBuilder.DropTable(
                name: "business_link_types");

            migrationBuilder.DropTable(
                name: "target_markets");

            migrationBuilder.DropTable(
                name: "employee_positions");

            migrationBuilder.DropTable(
                name: "person_identifier_types");

            migrationBuilder.DropTable(
                name: "business_roles");

            migrationBuilder.DropTable(
                name: "subscription_plans");

            migrationBuilder.DropTable(
                name: "app_sections");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "user_accounts");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "business_owners");

            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "business_members");

            migrationBuilder.DropTable(
                name: "businesses");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "address_types");

            migrationBuilder.DropTable(
                name: "business_statuses");

            migrationBuilder.DropTable(
                name: "business_types");

            migrationBuilder.DropTable(
                name: "currencies");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}

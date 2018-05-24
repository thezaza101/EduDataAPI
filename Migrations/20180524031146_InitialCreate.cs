using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EduDataAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeSet",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Payload = table.Column<string>(nullable: true),
                    PayloadType = table.Column<string>(nullable: true),
                    UpdateDescription = table.Column<string>(nullable: true),
                    UpdatedDomain = table.Column<string>(nullable: true),
                    iUpdatedIdentifiers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeSet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Domain",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    acronym = table.Column<string>(nullable: true),
                    domain = table.Column<string>(nullable: true),
                    rootSourceURL = table.Column<string>(nullable: true),
                    version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Facets",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    maxLength = table.Column<string>(nullable: true),
                    minInclusive = table.Column<string>(nullable: true),
                    minLength = table.Column<string>(nullable: true),
                    pattern = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Change",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    ChangeSetID = table.Column<string>(nullable: true),
                    ChangeType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Change", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Change_ChangeSet_ChangeSetID",
                        column: x => x.ChangeSetID,
                        principalTable: "ChangeSet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Datatype",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    facetsID = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datatype", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Datatype_Facets_facetsID",
                        column: x => x.facetsID,
                        principalTable: "Facets",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DataElement",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    DomainID = table.Column<string>(nullable: true),
                    datatypeID = table.Column<string>(nullable: true),
                    definition = table.Column<string>(nullable: true),
                    domain = table.Column<string>(nullable: true),
                    guidance = table.Column<string>(nullable: true),
                    identifier = table.Column<string>(nullable: true),
                    lastUpdateDate = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    sourceURL = table.Column<string>(nullable: true),
                    status = table.Column<string>(nullable: true),
                    version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataElement", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DataElement_Domain_DomainID",
                        column: x => x.DomainID,
                        principalTable: "Domain",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DataElement_Datatype_datatypeID",
                        column: x => x.datatypeID,
                        principalTable: "Datatype",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Change_ChangeSetID",
                table: "Change",
                column: "ChangeSetID");

            migrationBuilder.CreateIndex(
                name: "IX_DataElement_DomainID",
                table: "DataElement",
                column: "DomainID");

            migrationBuilder.CreateIndex(
                name: "IX_DataElement_datatypeID",
                table: "DataElement",
                column: "datatypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Datatype_facetsID",
                table: "Datatype",
                column: "facetsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Change");

            migrationBuilder.DropTable(
                name: "DataElement");

            migrationBuilder.DropTable(
                name: "ChangeSet");

            migrationBuilder.DropTable(
                name: "Domain");

            migrationBuilder.DropTable(
                name: "Datatype");

            migrationBuilder.DropTable(
                name: "Facets");
        }
    }
}

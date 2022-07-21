using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace GeradorTestes.Infra.Orm.Migrations
{
    public partial class AddTabelaTeste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBTeste",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(250)", nullable: false),
                    Provao = table.Column<bool>(type: "bit", nullable: false),
                    DataGeracao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisciplinaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MateriaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantidadeQuestoes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBTeste", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBTeste_TBDisciplina_DisciplinaId",
                        column: x => x.DisciplinaId,
                        principalTable: "TBDisciplina",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TBTeste_TBMateria_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "TBMateria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "QuestaoTeste",
                columns: table => new
                {
                    QuestoesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TestesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestaoTeste", x => new { x.QuestoesId, x.TestesId });
                    table.ForeignKey(
                        name: "FK_QuestaoTeste_TBQuestao_QuestoesId",
                        column: x => x.QuestoesId,
                        principalTable: "TBQuestao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestaoTeste_TBTeste_TestesId",
                        column: x => x.TestesId,
                        principalTable: "TBTeste",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestaoTeste_TestesId",
                table: "QuestaoTeste",
                column: "TestesId");

            migrationBuilder.CreateIndex(
                name: "IX_TBTeste_DisciplinaId",
                table: "TBTeste",
                column: "DisciplinaId");

            migrationBuilder.CreateIndex(
                name: "IX_TBTeste_MateriaId",
                table: "TBTeste",
                column: "MateriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestaoTeste");

            migrationBuilder.DropTable(
                name: "TBTeste");
        }
    }
}

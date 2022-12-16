using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooCode.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrationZooProjectMigrationContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    AnimalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animalname = table.Column<string>(name: "Animal_name", type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.AnimalID);
                });

            migrationBuilder.CreateTable(
                name: "Zoo",
                columns: table => new
                {
                    ZooID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Zooname = table.Column<string>(name: "Zoo_name", type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Zooaddress = table.Column<string>(name: "Zoo_address", type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zoo", x => x.ZooID);
                });

            migrationBuilder.CreateTable(
                name: "ZooAnimal",
                columns: table => new
                {
                    ZooAnimalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZooID = table.Column<int>(type: "int", nullable: false),
                    AnimalID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZooAnimal", x => x.ZooAnimalID);
                    table.ForeignKey(
                        name: "FK_ZooAnimal_Animal_AnimalID",
                        column: x => x.AnimalID,
                        principalTable: "Animal",
                        principalColumn: "AnimalID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZooAnimal_Zoo_ZooID",
                        column: x => x.ZooID,
                        principalTable: "Zoo",
                        principalColumn: "ZooID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ZooAnimal_AnimalID",
                table: "ZooAnimal",
                column: "AnimalID");

            migrationBuilder.CreateIndex(
                name: "IX_ZooAnimal_ZooID",
                table: "ZooAnimal",
                column: "ZooID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ZooAnimal");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Zoo");
        }
    }
}

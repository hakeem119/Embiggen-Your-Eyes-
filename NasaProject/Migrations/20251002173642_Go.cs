using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NasaProject.Migrations
{
    /// <inheritdoc />
    public partial class Go : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Annotations",
                columns: table => new
                {
                    AnnotationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LabelName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Geometry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annotations", x => x.AnnotationId);
                    table.ForeignKey(
                        name: "FK_Annotations_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Annotations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ImageMetadatas",
                columns: table => new
                {
                    MetadataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    Projection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinateSystem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResolutionX = table.Column<float>(type: "real", nullable: false),
                    ResolutionY = table.Column<float>(type: "real", nullable: false),
                    BandsCount = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageMetadatas", x => x.MetadataId);
                    table.ForeignKey(
                        name: "FK_ImageMetadatas_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessingJobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JobType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessingJobs", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_ProcessingJobs_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessingJobs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annotations_ImageId",
                table: "Annotations",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Annotations_UserId",
                table: "Annotations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageMetadatas_ImageId",
                table: "ImageMetadatas",
                column: "ImageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_UserId",
                table: "Images",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessingJobs_ImageId",
                table: "ProcessingJobs",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessingJobs_UserId",
                table: "ProcessingJobs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Annotations");

            migrationBuilder.DropTable(
                name: "ImageMetadatas");

            migrationBuilder.DropTable(
                name: "ProcessingJobs");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

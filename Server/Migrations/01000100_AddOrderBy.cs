using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using GIBS.Module.BannerCarousel.Migrations.EntityBuilders;
using GIBS.Module.BannerCarousel.Repository;

namespace GIBS.Module.BannerCarousel.Migrations
{

    [DbContext(typeof(BannerCarouselContext))]
    [Migration("GIBS.Module.BannerCarousel.01.00.01.00")]
    public class AddOrderBy : MultiDatabaseMigration
    {
        public AddOrderBy(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                 name: "OrderBy",
                 table: "GIBSBannerCarousel", // Replace with your table name
                 type: "int", // EF Core will typically map this correctly
                 nullable: false, // Set nullability as needed
                 defaultValue: 1); // Essential for non-nullable columns on existing data

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderBy",
                table: "GIBSBannerCarousel");
        }
    }
}
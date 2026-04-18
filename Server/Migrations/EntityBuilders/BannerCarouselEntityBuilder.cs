using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using Oqtane.Migrations.EntityBuilders;

namespace GIBS.Module.BannerCarousel.Migrations.EntityBuilders
{
    public class BannerCarouselEntityBuilder : AuditableBaseEntityBuilder<BannerCarouselEntityBuilder>
    {
        private const string _entityTableName = "GIBSBannerCarousel";
        private readonly PrimaryKey<BannerCarouselEntityBuilder> _primaryKey = new("PK_GIBSBannerCarousel", x => x.BannerCarouselId);
        private readonly ForeignKey<BannerCarouselEntityBuilder> _moduleForeignKey = new("FK_GIBSBannerCarousel_Module", x => x.ModuleId, "Module", "ModuleId", ReferentialAction.Cascade);

        public BannerCarouselEntityBuilder(MigrationBuilder migrationBuilder, IDatabase database) : base(migrationBuilder, database)
        {
            EntityTableName = _entityTableName;
            PrimaryKey = _primaryKey;
            ForeignKeys.Add(_moduleForeignKey);
        }

        protected override BannerCarouselEntityBuilder BuildTable(ColumnsBuilder table)
        {
            BannerCarouselId = AddAutoIncrementColumn(table,"BannerCarouselId");
            ModuleId = AddIntegerColumn(table,"ModuleId");
            Title = AddStringColumn(table,"Title", 200, true);
            Description = AddStringColumn(table, "Description", 400, true);
            ImageUrl = AddStringColumn(table, "ImageUrl", 400, true);
            ShowCaption = AddBooleanColumn(table, "ShowCaption", false, true);
            IsActive = AddBooleanColumn(table, "IsActive", false, true);
            AddAuditableColumns(table);
            return this;
        }

        public OperationBuilder<AddColumnOperation> BannerCarouselId { get; set; }
        public OperationBuilder<AddColumnOperation> ModuleId { get; set; }
        public OperationBuilder<AddColumnOperation> Title { get; set; }
        public OperationBuilder<AddColumnOperation> Description { get; set; }
        public OperationBuilder<AddColumnOperation> ImageUrl { get; set; }
        public OperationBuilder<AddColumnOperation> ShowCaption { get; set; }
        public OperationBuilder<AddColumnOperation> IsActive { get; set; }
    }
}
